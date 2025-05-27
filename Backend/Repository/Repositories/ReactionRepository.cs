using Contracts.IRepositories;
using Entities;
using Entities.Models.SocialFeed;

namespace Repository.Repositories
{
    public class ReactionRepository(RepositoryContext repositoryContext) : RepositoryBase<Reaction>(repositoryContext), IReactionRepository
    {
    }
}
