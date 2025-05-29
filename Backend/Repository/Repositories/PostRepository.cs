using Core.Interfaces;
using Entities;
using Entities.Models.SocialFeed;

namespace Repository.Repositories
{
    public class PostRepository(RepositoryContext repositoryContext) : RepositoryBase<Post>(repositoryContext), IPostRepository
    {
    }
}
