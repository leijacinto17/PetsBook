using Contracts.IRepositories;
using Entities;
using Entities.Models.SocialFeed;

namespace Repository.Repositories
{
    public class AttachmentRepository(RepositoryContext repositoryContext) : RepositoryBase<Attachment>(repositoryContext), IAttachmentRepository
    {
    }
}
