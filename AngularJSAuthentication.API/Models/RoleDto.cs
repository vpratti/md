using System.Runtime.Serialization;

namespace AngularJSAuthentication.API.Models
{
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
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}