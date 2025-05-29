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

    }
}
