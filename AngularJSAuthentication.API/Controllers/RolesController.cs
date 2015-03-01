using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthContext _context;

        public RolesController()
        {
            _context = new AuthContext();
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
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
    }
}