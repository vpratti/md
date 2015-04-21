using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class LookupValueDto
    {
        public long Id { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public List<LookupAliasDto> LookupAliases { get; set; }
    }
}