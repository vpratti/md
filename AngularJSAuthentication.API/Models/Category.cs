namespace AngularJSAuthentication.API.Models
{
    public class Category : LookupBase
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}