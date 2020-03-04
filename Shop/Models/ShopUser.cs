using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ShopUser : IdentityUser
    {
        public virtual Cart Cart { get; set; }
    }
}
