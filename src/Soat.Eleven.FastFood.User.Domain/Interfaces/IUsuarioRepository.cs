using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<bool> ExistEmail(string email);
}
