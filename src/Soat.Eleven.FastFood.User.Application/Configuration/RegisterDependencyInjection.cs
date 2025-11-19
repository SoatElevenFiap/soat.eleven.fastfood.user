using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.Handlers;
using Soat.Eleven.FastFood.User.Application.Interfaces.Handlers;
using Soat.Eleven.FastFood.User.Application.Validators;

namespace Soat.Eleven.FastFood.User.Application.Configuration;

public static class RegisterDependencyInjection
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioHandler, UsuarioHandler>();
        services.AddScoped<IClienteHandler, ClienteHandler>();
        services.AddScoped<IAdministradorHandler, AdministradorHandler>();
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CriarClienteInputDto>, CriarClienteValidator>();
        services.AddTransient<IValidator<CriarAdmInputDto>, CriarAdmValidator>();
        services.AddTransient<IValidator<AtualizaAdmInputDto>, AtualizaAdmValidator>();
        services.AddTransient<IValidator<AtualizaClienteInputDto>, AtualizaClienteValidator>();
        services.AddTransient<IValidator<AtualizarSenhaInputDto>, AtualizarSenhaValidator>();
    }
}
