using System.Linq.Expressions;

namespace UniTrackBackend.Data.Commons;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    public Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

    Task LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, ICollection<TProperty>>> navigationProperty) where TProperty : class;

    Task LoadReferenceAsync<TProperty>(T entity, Expression<Func<T, TProperty>> navigationProperty) where TProperty : class;
    
}