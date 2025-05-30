using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.UnitOfWork
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private Mock<DataContext> _mockContext;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _mockContext = new Mock<DataContext>(options);
        }

        [TestMethod]
        public async Task SaveChangesAsync_Should_Call_DbContext_SaveChangesAsync()
        {
            _mockContext.Setup(c => c.SaveChangesAsync(default))
                .ReturnsAsync(1);

            var unitOfWork = new Infrastructure.Data.UnitOfWork(_mockContext.Object);

            var result = await unitOfWork.SaveChangesAsync();

            Assert.IsTrue(result);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [TestMethod]
        public void UserRepository_Should_Return_Instance()
        {
            var unitOfWork = new Infrastructure.Data.UnitOfWork(_mockContext.Object);

            var repo = unitOfWork.User;

            Assert.IsNotNull(repo);
            Assert.IsInstanceOfType(repo, typeof(IUserRepository));
        }

        [TestMethod]
        public void Dispose_Should_Dispose_Context()
        {
            var unitOfWork = new Infrastructure.Data.UnitOfWork(_mockContext.Object);

            unitOfWork.Dispose();

            _mockContext.Verify(c => c.Dispose(), Times.Once);
        }
    }
}
