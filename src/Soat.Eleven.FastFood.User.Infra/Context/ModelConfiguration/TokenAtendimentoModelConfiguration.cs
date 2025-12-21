using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Infra.Context.ModelConfiguration;

public class TokenAtendimentoModelConfiguration : EntityBaseModelConfiguration<TokenAtendimento>
{
    public override void Configure(EntityTypeBuilder<TokenAtendimento> builder)
    {
        base.Configure(builder);

        builder.ToTable("TokensAtendimento");
    }
}
