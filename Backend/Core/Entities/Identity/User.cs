using Core.Entities.SocialFeed;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            
        }

        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public IdentityResult AddRole(UserManager<User> userManager, string RoleName)
        {
            return userManager.AddToRoleAsync(this, RoleName).Result;
        }
        public Task<string> GeneratePasswordResetToken(UserManager<User> userManager)
        {
            return userManager.GeneratePasswordResetTokenAsync(this);
        }
        public Task<string> GenerateEmailConfirmationToken(UserManager<User> userManager)
        {
            return userManager.GenerateEmailConfirmationTokenAsync(this);
        }

    }
}
