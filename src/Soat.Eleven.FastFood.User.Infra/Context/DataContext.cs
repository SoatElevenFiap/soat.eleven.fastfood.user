using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Infra.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<TokenAtendimento> TokenAtendimento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
