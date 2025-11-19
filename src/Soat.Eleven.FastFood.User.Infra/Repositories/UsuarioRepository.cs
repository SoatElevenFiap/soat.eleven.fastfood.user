using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> ExistEmail(string email)
    {
        var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        return user is not null;
    }
}
