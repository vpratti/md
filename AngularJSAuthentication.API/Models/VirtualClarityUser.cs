using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Models
{
    public class VirtualClarityUser : IdentityUser
    {
        public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}