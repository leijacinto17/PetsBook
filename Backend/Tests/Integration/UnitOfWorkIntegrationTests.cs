using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Integration
{
    [TestClass]

    public class UnitOfWorkIntegrationTests
    {
        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [TestMethod]
        public async Task Can_Add_And_Get_User_Using_UnitOfWork()
        {
            await using var context = GetInMemoryContext();
            var unitOfWork = new Infrastructure.Data.UnitOfWork(context);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser"
            };

            await unitOfWork.User.CreateAsync(user);
            await unitOfWork.SaveChangesAsync();

            var retrievedUser = await unitOfWork.User.FindAsync(user.Id);

            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("testuser", retrievedUser.UserName);
        }
    }
}
