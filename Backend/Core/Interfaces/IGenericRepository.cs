using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T?> FindAsync(object id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
