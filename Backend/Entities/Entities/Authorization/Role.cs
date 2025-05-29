using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Authorization
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
