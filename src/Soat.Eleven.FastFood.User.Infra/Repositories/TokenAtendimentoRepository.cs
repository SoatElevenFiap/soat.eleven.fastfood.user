using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public class TokenAtendimentoRepository : BaseRepository<TokenAtendimento>, ITokenAtendimentoRepository
{
    public TokenAtendimentoRepository(DataContext context) : base(context)
    {
    }
}
