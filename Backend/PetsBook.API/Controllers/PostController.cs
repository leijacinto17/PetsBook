using Microsoft.AspNetCore.Mvc;
using PetsBook.API.Common;

namespace PetsBook.API.Controllers
{
    public class PostController : BaseApiController<PostController>
    {
        public PostController(ILogger<PostController> logger) : base(logger) { }

        [HttpGet]
        public IActionResult TestLogging()
        {
            try
            {
                LogInfo("TestLogging endpoint was called.");

                // Simulate some logic
                var posts = new[] { "Post 1", "Post 2", "Post 3" };

                return Ok(new
                {
                    Message = "Logging test successful",
                    Data = posts
                });
            }
            catch (Exception ex)
            {
                LogError(ex);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
