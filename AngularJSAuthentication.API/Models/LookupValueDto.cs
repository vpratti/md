﻿namespace AngularJSAuthentication.API.Models
{
    public class LookupValueDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long MasterLookupValueId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}