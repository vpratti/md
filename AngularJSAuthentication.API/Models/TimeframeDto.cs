using System;
using System.Collections.Generic;

namespace AngularJSAuthentication.API.Models
{
    public class TimeframeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public long Id { get; set; }

        public List<TimeframeChildDto> TimeframeChildren { get; set; }
    }
}