using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Database;

namespace UniTrackBackend.Data.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly UniTrackDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(UniTrackDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GeAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = _dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        
        return await query.ToListAsync();
        
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }


    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
}



