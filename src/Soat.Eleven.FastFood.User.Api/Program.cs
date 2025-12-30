using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Soat.Eleven.FastFood.User.Api.Configuration;
using Soat.Eleven.FastFood.User.Domain.Enums;
using Soat.Eleven.FastFood.User.Infra.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

try 
{
    builder.ConfigureKeyVault();
}
catch (Exception ex)
{
    Console.WriteLine($"[ERRO CRÍTICO] Falha ao conectar no Key Vault: {ex.Message}");
}

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.RegisterRepositories();
builder.Services.RegisterValidators();
builder.Services.RegisterServices();
builder.Services.RegisterHandlers();

builder.Services.AddCors();

// 2. Configuração de Autenticação com Fallback de Chave
var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"];
if (string.IsNullOrEmpty(jwtSecretKey))
{
    Console.WriteLine("[AVISO] JwtSettings:SecretKey não encontrada. Usando chave temporária para evitar crash.");
    jwtSecretKey = "ChaveDeSegurancaTemporariaParaFinsDeDebug123!"; 
}

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Cliente", policy => policy.RequireRole(RolesAuthorization.Cliente))
    .AddPolicy("Administrador", policy => policy.RequireRole(RolesAuthorization.Administrador))
    .AddPolicy("ClienteTotem", policy => policy.RequireRole(RolesAuthorization.Cliente, RolesAuthorization.IdentificacaoTotem))
    .AddPolicy("Commom", policy => policy.RequireRole(RolesAuthorization.Cliente, RolesAuthorization.Administrador));

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

var app = builder.Build();

// 3. MAPEAR HEALTH CHECK NO TOPO - Resolve o 404 das Probes do Kubernetes
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();
app.UseStaticFiles();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

// Desabilitado redirecionamento HTTPS para evitar erro de porta no cluster AKS
// app.UseHttpsRedirection(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("=== Aplicação Inicializada com Sucesso ===");
await app.RunAsync();