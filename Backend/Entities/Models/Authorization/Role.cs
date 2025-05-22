using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
