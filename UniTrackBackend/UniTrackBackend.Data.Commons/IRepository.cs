using System.Linq.Expressions;

namespace UniTrackBackend.Data.Commons;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GeAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    public Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}