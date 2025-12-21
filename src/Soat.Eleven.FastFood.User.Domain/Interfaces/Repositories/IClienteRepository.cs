using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente?> GetByCPF(string cpf);
    Task<bool> ExistByCpf(string cpf);
}
