using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Infra.Context.ModelConfiguration;

public class UsuarioModelConfiguration : EntityBaseModelConfiguration<Usuario>
{
    private static Usuario UsuarioAdmDefault
    {
        get
        {
            return new Usuario
            {
                Id = Guid.Parse("3b31ada8-b56a-466d-a1a6-75fe92a36552"),
                Nome = "Sistema Fast Food",
                Email = "sistema@fastfood.com",
                Senha = "3+wuaNtvoRoxLxP7qPmYrg==",
                Telefone = "11985203641",
                Perfil = PerfilUsuario.Administrador
            };
        }
    }

    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("Usuarios");

        builder.Property(c => c.Nome)
               .IsRequired();

        builder.Property(c => c.Email)
               .IsRequired();

        builder.Property(c => c.Perfil)
               .IsRequired();

        builder.Property(c => c.Perfil)
               .HasConversion<string>();

        builder.Property(c => c.Status)
               .HasDefaultValue(StatusUsuario.Ativo)
               .HasConversion<string>();


        builder.HasData(UsuarioAdmDefault);
    }
}
