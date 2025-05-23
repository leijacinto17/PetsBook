using Contracts.IRepositories;
using Entities;
using Entities.Models.Feeds;

namespace Repository.Repositories
{
    public class AttachmentRepository(RepositoryContext repositoryContext) : RepositoryBase<Attachment>(repositoryContext), IAttachmentRepository
    {
    }
}
