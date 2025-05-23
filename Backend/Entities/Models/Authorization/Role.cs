using Microsoft.AspNetCore.Identity;

namespace Entities.Models.Authorization
{
    public class Role : IdentityRole<string>
    {
        public Role()
        {
            // stub
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
