using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public static class SeedData
    {
        public static void Roles(RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new string[] { "Admin", "User" };

            foreach( var roleName in roleNames )
            { 
                if( !roleManager.RoleExistsAsync(roleName).Result )
                {
                    var role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }
    }
}
