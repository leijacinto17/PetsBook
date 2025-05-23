using Contracts.IRepositories;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? User { get; }
        IPostRepository Post { get; }

        Task<bool> SaveChangesAsync();
    }
}
