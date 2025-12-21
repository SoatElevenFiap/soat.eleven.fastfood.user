using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Infra.Context.ModelConfiguration;

public class EntityBaseModelConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.CriadoEm)
           .HasColumnType("timestamp")
           .HasDefaultValueSql("NOW()");

        builder.Property(c => c.ModificadoEm)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("NOW()")
               .ValueGeneratedOnAddOrUpdate();
    }
}
