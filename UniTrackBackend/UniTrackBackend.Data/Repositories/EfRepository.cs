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

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
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

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate);
    }
    
    public async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty) where TProperty : class
    {
        // Extract the property name from the lambda expression
        var propertyName = GetPropertyName(navigationProperty);

        var collection = _context.Entry(entity).Collection(propertyName);
        if (!collection.IsLoaded)
        {
            await collection.LoadAsync();
        }
    }

    public async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty) where TProperty : class
    {
        var reference = _context.Entry(entity).Reference(navigationProperty);
        if (!reference.IsLoaded)
        {
            await reference.LoadAsync();
        }
    }
    
    private static string GetPropertyName<TProperty>(Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty)
    {
        if (navigationProperty.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        throw new ArgumentException("Invalid navigation property expression", nameof(navigationProperty));
    }

}



