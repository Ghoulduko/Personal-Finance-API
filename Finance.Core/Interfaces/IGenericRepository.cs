using System.Linq.Expressions;

namespace Finance.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<bool> CheckExistenceAsync(Expression<Func<T, bool>> predicate);
    Task DeleteAsync(T entity);
    Task SaveAsync();
}