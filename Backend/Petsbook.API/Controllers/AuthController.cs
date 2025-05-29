using Application.Jsons;
using Application.Services;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetsBook.API.Common;
using Presentation.ViewModels.User;
using System.IdentityModel.Tokens.Jwt;

namespace Petsbook.API.Controllers
{
    public class AuthController : BaseApiController<AuthController>
    {
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager) : base(logger, unitOfWork, userManager) 
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        // POST: api/Auth/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                string message = "Invalid username or password.";
                return BadRequest(new JsonGenericResult { ReturnCode = 1, Message = message });
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    // Create the token descriptor for the JWT access token
                    IList<string> roles = _userManager.GetRolesAsync(user).Result;
                    var tokenDiscriptor = await AuthService.CreateTokenInternalLoginAsync(
                        user,
                        roles,
                        _userManager,
                        model.Password);

                    // Create the access token for the authentication
                    var tokenHandler = new JwtSecurityTokenHandler();

                    if (tokenDiscriptor != null)
                    {
                        SecurityToken token = tokenHandler.CreateToken(tokenDiscriptor);

                        LogInfo($"{user.UserName} has been successfully logged in at {DateTime.Now}.");

                        return Ok(new
                        {
                            token = tokenHandler.WriteToken(token),
                            expiration = token.ValidTo,
                            username = user.UserName,
                            Roles = roles,
                        });
                    }
                    else
                    {
                        string message = "Something went wrong with your authentication, please try again.";
                        return BadRequest(new JsonGenericResult() { ReturnCode = 400, Message = message });
                    }
                }
                if (!result.IsLockedOut && user.AccessFailedCount <= 3)
                {
                    string message = "Invalid username or password.";
                    return BadRequest(new JsonGenericResult() { ReturnCode = 6, Message = message });
                }
                if (user.AccessFailedCount == 4)
                {
                    LogWarn($"WARNING: Last attempt for this account, {user.UserName}, from logging in. Number of attempt/s: {user.AccessFailedCount}");
                    string message = "WARNING: Last attempt for this account! For invalid attempt, This account will be locked out for 5 minutes!";
                    return BadRequest(new JsonGenericResult() { ReturnCode = 7, Message = message });
                }
                else
                {
                    LogWarn($"This account of {user.UserName} have been locked out for 5 minutes due to invalid attempts. Number of attempt/s: {user.AccessFailedCount}");
                    string message = "This account have been locked out for 5 minutes due to invalid attempts.";
                    return BadRequest(new JsonGenericResult() { ReturnCode = 8, Message = message });
                }
            }
            catch (Exception ex)
            {
                // If something went wrong
                LogError(ex);
                string message = "Something went wrong with your authentication, please try again.";
                return BadRequest(new JsonGenericResult() { ReturnCode = 400, Message = message });
            }
        }
    }
}