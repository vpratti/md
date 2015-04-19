using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.DbContexts;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Seterlund.CodeGuard;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthContext _context;

        public RolesController()
        {
            _context = new AuthContext();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));

        }

        [HttpGet]
        [Authorize]
        [Route("GetRoles")]
        public async Task<IHttpActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToArrayAsync();
            var rolesDto = new List<RoleDto>();

            roles.ForEach(i => rolesDto.Add(new RoleDto(i.Id, i.Name)));

            return Ok(rolesDto);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateRole")]
        public async Task<IHttpActionResult> CreateRole(string roleName)
        {
            var role = new IdentityRole
            {
                Name = roleName
            };

            var result = await _roleManager.CreateAsync(role);
            var rolesDto = new List<RoleDto>();

            if (result.Succeeded)
            {
                var roles = await _roleManager.Roles.ToArrayAsync();
                roles.ForEach(i => rolesDto.Add(new RoleDto(i.Id, i.Name)));
            }

            return Ok(rolesDto);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteRole")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            Guard.That(id).IsNotEqual(_roleManager.FindByName("admin").Id);

            IdentityRole role = _roleManager.Roles.First(i => i.Id.Equals(id));
            await _roleManager.DeleteAsync(role);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserRoles")]
        public async Task<IHttpActionResult> GetUserRoles(string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);
            var roles = new List<RoleDto>();

            user.Roles.ForEach(i =>
            {
                var role = _roleManager.FindById(i.RoleId);
                roles.Add(new RoleDto(i.RoleId, role.Name));
            });

            return Ok(roles);
        }
    }
}