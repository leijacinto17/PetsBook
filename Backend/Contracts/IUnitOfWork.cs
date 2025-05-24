using Contracts.IRepositories;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository? User { get; }
        IPostRepository Post { get; }
        IAttachmentRepository Attachment { get; }
        IReactionRepository Reaction { get; }

        Task<bool> SaveChangesAsync();
    }
}
