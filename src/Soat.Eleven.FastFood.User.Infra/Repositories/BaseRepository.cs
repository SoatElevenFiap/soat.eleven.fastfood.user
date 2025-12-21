using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Infra.Context;

namespace Soat.Eleven.FastFood.User.Infra.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        IQueryable<TEntity> query = _dbSet;

        return await query
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
