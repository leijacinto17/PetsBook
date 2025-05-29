using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PetsBook.API.Common
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly UserManager<User> _userManager;

        protected BaseApiController(ILogger<T> logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // You can put shared logic, services, etc. here
        protected void LogInfo(string message) => _logger.LogInformation(message);

        protected void LogError(Exception ex) => _logger.LogError(ex, ex.Message);

        protected void LogWarn(Exception ex) => _logger.LogWarning(ex, ex.Message);

        protected void LogWarn(string message) => _logger.LogWarning(message);

    }
}
