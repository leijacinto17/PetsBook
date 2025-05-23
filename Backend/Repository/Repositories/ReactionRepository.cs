using Contracts.IRepositories;
using Entities;
using Entities.Models.Feeds;

namespace Repository.Repositories
{
    public class ReactionRepository(RepositoryContext repositoryContext) : RepositoryBase<Reaction>(repositoryContext), IReactionRepository
    {
    }
}
