using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.Models
{
    public sealed class VirtualClarityUser : IdentityUser
    {
        public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public VirtualClarityUser() { }

        public VirtualClarityUser(string username, string email, string firstName, string lastName, 
            bool lockoutEnabled)
        {
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            LockoutEnabled = lockoutEnabled;
        }

        public VirtualClarityUser(UserModel userModel)
        {
            UserName = userModel.UserName;
            Email = userModel.Email;
            FirstName = userModel.FirstName;
            LastName = userModel.LastName;
            PhoneNumber = userModel.PhoneNumber.ToString();
        }
    }
}