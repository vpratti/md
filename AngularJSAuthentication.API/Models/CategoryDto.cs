namespace AngularJSAuthentication.API.Models
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }

        public CategoryTypeDto Type { get; set; }
    }
}