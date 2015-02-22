using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AngularJSAuthentication.API.Models
{
    [DataContract]
    public class UserDto
    {
        public UserDto() { }

        public UserDto(string id, string username, IEnumerable<Role> roles)
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
        public IEnumerable<Role> Roles { get; set; }
    }

    [DataContract]
    public class Role
    {
        public Role() { }

        public Role(string id, string name)
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