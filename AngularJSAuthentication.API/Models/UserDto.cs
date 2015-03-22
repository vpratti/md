using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AngularJSAuthentication.API.Models
{
    [DataContract]
    public class UserDto
    {
        public UserDto() { }

        public UserDto(string id, string username, IEnumerable<RoleDto> roles, string email, string firstName, string lastName, string phoneNumber)
        {
            Id = id;
            UserName = username;
            Roles = roles;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public IEnumerable<RoleDto> Roles { get; set; }

        [DataMember]
        public string Email { get; set; } 
    }
}