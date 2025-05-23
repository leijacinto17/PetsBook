using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T?> FindAsync(object id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
