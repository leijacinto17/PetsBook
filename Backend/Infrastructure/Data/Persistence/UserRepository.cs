using Core.Entities.Identity;
using Core.Interfaces;

namespace Infrastructure.Data.Persistence
{
    public class UserRepository(DataContext context) : GenericRepository<User>(context), IUserRepository
    {
        public bool CheckAnyUsers()
        {
            return context.Set<User>().Any();
        }
    }
}
