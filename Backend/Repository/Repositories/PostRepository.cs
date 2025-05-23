using Contracts;
using Contracts.IRepositories;
using Entities;
using Entities.Models.Feeds;

namespace Repository.Repositories
{
    public class PostRepository(RepositoryContext repositoryContext) : RepositoryBase<Post>(repositoryContext), IPostRepository
    {
    }
}
