using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    Guid GetUsuarioId();
    Usuario? GetUsuario();
}
