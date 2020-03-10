using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shop.Models;

namespace Shop.Services
{
    public class UserManager : IUserManager
    {
        private readonly SignInManager<ShopUser> _signInManager;

        public UserManager(SignInManager<ShopUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
    }
}
