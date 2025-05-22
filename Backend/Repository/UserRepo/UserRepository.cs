using Contracts.IUser;
using Entities;
using Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateUser(User user)
        {
            RepositoryContext.Set<User>().Add(user);
        }

        public void UpdateUser(User user)
        {
            RepositoryContext.Set<User>().Update(user);
        }

        public void DeleteUser(User user)
        {
            RepositoryContext.Set<User>().Remove(user);
            Save();
        }
    }
}
