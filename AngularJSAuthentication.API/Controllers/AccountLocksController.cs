using System;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Constants;
using AngularJSAuthentication.API.DbContexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/AccountLocks")]
    public class AccountLocksController : ApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthContext _context;

        public AccountLocksController()
        {
            _context = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("Unlock")]
        public async Task<IHttpActionResult> Unlock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.LockoutEndDateUtc = null;

            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("Lock")]
        public async Task<IHttpActionResult> Lock(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (!user.UserName.Equals(UserConstants.Admin))
            {
                user.LockoutEndDateUtc = new DateTime(3000, 1, 1);

                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }
    }
}