using System;

namespace AngularJSAuthentication.API.Models
{
    public class TimeframeChildDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public long Id { get; set; }
    }
}