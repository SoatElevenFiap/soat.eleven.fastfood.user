using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(DataContext context) : base(context)
    {
    }
}
