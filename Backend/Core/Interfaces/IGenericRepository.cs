using System.Linq.Expressions;
using Core.Entities;

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
