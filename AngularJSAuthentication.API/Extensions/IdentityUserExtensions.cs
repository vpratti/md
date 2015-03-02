using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Extensions
{
    public static class IdentityUserExtensions
    {
        public static void RemoveAllRoles(this IdentityUser identityUser)
        {
            var oldRoles = new List<IdentityUserRole>();
            oldRoles.AddRange(identityUser.Roles);
            oldRoles.ForEach(i => identityUser.Roles.Remove(i));
        }
    }
}