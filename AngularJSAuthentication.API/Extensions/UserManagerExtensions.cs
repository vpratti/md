using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static IdentityUser GetUserByEmail(this UserManager<IdentityUser> userManager, string email)
        {
            IdentityUser user =
                userManager.Users.First(i => !string.IsNullOrEmpty(i.Email) && i.Email.Equals(email));

            return user;
        }
    }
}