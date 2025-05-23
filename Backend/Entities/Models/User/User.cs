using Entities.Models.Feeds;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
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
