using Core.Entities.SocialFeed;
using Core.Interfaces;

namespace Infrastructure.Data.Persistence
{
    public class PostRepository(DataContext context) : GenericRepository<Post>(context), IPostRepository
    {
    }
}
