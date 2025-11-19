using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> ExistByCpf(string cpf)
    {
        var cliente = await _dbSet.FirstOrDefaultAsync(c => c.Cpf == cpf);
        return cliente is not null;
    }

    public async Task<Cliente?> GetByCPF(string cpf)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Cpf == cpf);
    }
}
