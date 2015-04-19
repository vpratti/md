namespace AngularJSAuthentication.API.Models
{
    public class LookupAliasDto
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public long LookupValueId { get; set; }

        public long Id { get; set; }
    }
}