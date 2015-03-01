using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AngularJSAuthentication.API.Models
{
    [DataContract]
    public class UserDto
    {
        public UserDto() { }

        public UserDto(string id, string username, IEnumerable<RoleDto> roles, string email)
        {
            Id = id;
            UserName = username;
            Roles = roles;
            Email = email;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public IEnumerable<RoleDto> Roles { get; set; }

        [DataMember]
        public string Email { get; set; } 
    }
}