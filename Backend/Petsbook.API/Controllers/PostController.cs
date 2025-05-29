using Core.Entities.Identity;
using Core.Entities.SocialFeed;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsBook.API.Common;

namespace PetsBook.API.Controllers
{
    public class PostController : BaseApiController<PostController>
    {
        public PostController(ILogger<PostController> logger, IUnitOfWork unitOfWork, UserManager<User> userManager) : base(logger, unitOfWork, userManager) { }


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

        [HttpGet]
        public async Task<IActionResult> TestGetPost()
        {
            var posts = await _unitOfWork.Post.FindAll().ToListAsync();

            return Ok(new
            {
                Data = posts
            });
        }


        [HttpPost]
        public async Task<IActionResult> TestCreatePost()
        {
            var post = new Post
            {
                UserId = "1c3e6044-3d1a-4c8e-a904-952bf74872cc",
                Content = "This is for testing purposes only!",
            };

            await _unitOfWork.Post.CreateAsync(post);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new
            {
                Message = "Successfully created"
            });
        }
    }
}
