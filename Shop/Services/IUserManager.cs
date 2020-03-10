using System.Security.Claims;

namespace Shop.Web.Services
{
    public interface IUserManager
    {
        bool IsSignedIn(ClaimsPrincipal user);
    }
}
