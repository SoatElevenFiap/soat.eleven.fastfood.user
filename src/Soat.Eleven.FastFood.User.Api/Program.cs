using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Soat.Eleven.FastFood.User.Api.Configuration;
using Soat.Eleven.FastFood.User.Domain.Enums;
using Soat.Eleven.FastFood.User.Infra.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.ConfigureKeyVault();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.RegisterRepositories();
builder.Services.RegisterValidators();
builder.Services.RegisterServices();
builder.Services.RegisterHandlers();

builder.Services.AddCors();

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
            // Agora busca a chave do sistema de configura��o (que inclui o Key Vault)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Cliente", policy => policy.RequireRole(RolesAuthorization.Cliente))
    .AddPolicy("Administrador", policy => policy.RequireRole(RolesAuthorization.Administrador))
    .AddPolicy("ClienteTotem", policy => policy.RequireRole([RolesAuthorization.Cliente, RolesAuthorization.IdentificacaoTotem]))
    .AddPolicy("Commom", policy => policy.RequireRole([RolesAuthorization.Cliente, RolesAuthorization.Administrador]));

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map Health Check endpoints
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();