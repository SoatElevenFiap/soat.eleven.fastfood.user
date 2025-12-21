using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Infra.Context.ModelConfiguration;

public class ClienteModelConfiguration : EntityBaseModelConfiguration<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> builder)
    {
        base.Configure(builder);

        builder.ToTable("Clientes");

        builder.HasOne(c => c.Usuario)
               .WithOne(u => u.Cliente)
               .HasForeignKey<Cliente>(c => c.UsuarioId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.Property(c => c.Cpf)
               .IsRequired()
               .HasMaxLength(11);

        builder.Property(c => c.DataDeNascimento)
               .IsRequired()
               .HasColumnType("timestamp");
    }
}
