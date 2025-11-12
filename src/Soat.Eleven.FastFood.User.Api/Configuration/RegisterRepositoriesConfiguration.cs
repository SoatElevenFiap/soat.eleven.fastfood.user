using Soat.Eleven.FastFood.User.Domain.Interfaces;
using Soat.Eleven.FastFood.User.Infra.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class RegisterRepositoriesConfiguration
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<ITokenAtendimentoRepository, TokenAtendimentoRepository>();
    }
}
