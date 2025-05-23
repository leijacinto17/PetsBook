﻿using Contracts.IRepositories;
using Entities;
using Entities.Models;

namespace Repository.Repositories
{
    public class UserRepository(RepositoryContext repositoryContext) : RepositoryBase<User>(repositoryContext), IUserRepository
    {
    }
}
