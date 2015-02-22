using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API
{
    public class AuthContextInitializer : CreateDatabaseIfNotExists<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            const string name = "admin";
            const string password = "password";

            if (!roleManager.RoleExists(name))
            {
                roleManager.Create(new IdentityRole(name));
            }

            var user = new IdentityUser {UserName = name};
            var adminresult = userManager.Create(user, password);

            if (adminresult.Succeeded)
            {
                userManager.AddToRole(user.Id, name);
            }

            base.Seed(context);
        }
    }
}