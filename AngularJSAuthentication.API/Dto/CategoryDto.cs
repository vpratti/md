using System.Collections.Generic;

namespace AngularJSAuthentication.API.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public List<LookupValueDto> Values { get; set; }
    }
}