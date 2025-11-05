using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public class BaseRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DataContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    protected async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    protected async Task<TEntity?> GetByIdAsync(Guid id)
    {
        IQueryable<TEntity> query = _dbSet;

        return await query
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    protected async void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
