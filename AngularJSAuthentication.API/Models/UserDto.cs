using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AngularJSAuthentication.API.Models
{
    [DataContract]
    public class UserDto
    {
        public UserDto() { }

        public UserDto(string id, string username, IEnumerable<RoleDto> roles)
        {
            Id = id;
            UserName = username;
            Roles = roles;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public IEnumerable<RoleDto> Roles { get; set; }
    }

    [DataContract]
    public class RoleDto
    {
        public RoleDto() { }

        public RoleDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        [DataMember]
        string Id { get; set; }

        [DataMember]
        string Name { get; set; }
    }
}