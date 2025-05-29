using Core.Entities.SocialFeed;
using Core.Interfaces;

namespace Infrastructure.Data.Persistence
{
    public class ReactionRepository(DataContext context) : GenericRepository<Reaction>(context), IReactionRepository
    {
    }
}
