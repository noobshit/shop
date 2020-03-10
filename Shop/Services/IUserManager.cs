using System.Security.Claims;

namespace Shop.Services
{
    public interface IUserManager
    {
        bool IsSignedIn(ClaimsPrincipal user);
    }
}
