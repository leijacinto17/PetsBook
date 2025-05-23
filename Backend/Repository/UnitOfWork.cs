using Contracts;
using Contracts.IRepositories;
using Entities;
using Repository.Repositories;

namespace Repository
{
    public class UnitOfWork(RepositoryContext repositoryContext) : IUnitOfWork
    {
        private readonly RepositoryContext _repositoryContext = repositoryContext;
        private readonly IUserRepository? _userRepository;

        public IUserRepository User => _userRepository ?? new UserRepository(_repositoryContext);

        public async Task<bool> SaveChangesAsync()
        {
            return await _repositoryContext.SaveChangesAsync() > 0;
        }

        public void Dispose()   
        {
            _repositoryContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
