using Contracts;
using Contracts.IRepositories;
using Entities;
using Repository.Repositories;

namespace Repository
{
    public class UnitOfWork(RepositoryContext repositoryContext) : IUnitOfWork
    {
        private readonly RepositoryContext _repositoryContext = repositoryContext;

        private readonly IUserRepository? _userRepository;
        private readonly IPostRepository? _postRepository;
        private readonly IAttachmentRepository? _attachmentRepository;
        private readonly IReactionRepository? _reactionRepository;

        public IUserRepository User => _userRepository ?? new UserRepository(_repositoryContext);
        public IPostRepository Post => _postRepository ?? new PostRepository(_repositoryContext);
        public IAttachmentRepository Attachment => _attachmentRepository ?? new AttachmentRepository(_repositoryContext);
        public IReactionRepository Reaction => _reactionRepository ?? new ReactionRepository(_repositoryContext);

        public async Task<bool> SaveChangesAsync()
        {
            return await _repositoryContext.SaveChangesAsync() > 0;
        }

        public void Dispose()   
        {
            _repositoryContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
