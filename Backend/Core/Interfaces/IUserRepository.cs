using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public bool CheckAnyUsers();
    }
}
