using Entities.Models;

namespace Contracts.IRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        public bool CheckAnyUsers();
    }
}
