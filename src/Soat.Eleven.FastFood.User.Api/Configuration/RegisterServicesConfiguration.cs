using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;
using Soat.Eleven.FastFood.User.Infra.Services;

namespace Soat.Eleven.FastFood.User.Api.Configuration;

public static class RegisterServicesConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<IPasswordService, PasswordService>();
    }
}
