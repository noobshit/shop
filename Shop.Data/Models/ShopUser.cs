using Microsoft.AspNetCore.Identity;

namespace Shop.Data.Models
{
    public class ShopUser : IdentityUser
    {
        public virtual Cart Cart { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
