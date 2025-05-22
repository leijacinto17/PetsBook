using Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IUser
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
