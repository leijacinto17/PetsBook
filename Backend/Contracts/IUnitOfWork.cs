using Contracts.IRepositories;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? User { get; }

        Task<bool> SaveChangesAsync();
    }
}
