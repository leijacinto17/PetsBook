using Entities.Models.Feeds;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            
        }

        public virtual ICollection<Reaction> Reactions { get; set; } = [];
        public virtual ICollection<Post> Posts { get; set; } = [];

    }
}
