using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PetsBook.API.Common
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseApiController(ILogger<T> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        // You can put shared logic, services, etc. here

        protected void LogInfo(string message) => _logger.LogInformation(message);

        protected void LogError(Exception ex) => _logger.LogError(ex, ex.Message);
    }
}
