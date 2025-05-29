using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Persistence;

namespace Repository
{
    public class UnitOfWork(DataContext context) : IUnitOfWork
    {
        private readonly IUserRepository? _userRepository;
        private readonly IPostRepository? _postRepository;
        private readonly IAttachmentRepository? _attachmentRepository;
        private readonly IReactionRepository? _reactionRepository;

        public IUserRepository User => _userRepository ?? new UserRepository(context);
        public IPostRepository Post => _postRepository ?? new PostRepository(context);
        public IAttachmentRepository Attachment => _attachmentRepository ?? new AttachmentRepository(context);
        public IReactionRepository Reaction => _reactionRepository ?? new ReactionRepository(context);

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()   
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
