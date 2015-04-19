using System;

namespace AngularJSAuthentication.API.Models
{
    public class NewTimeframeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public TimeframeDto ParentTimeFrame { get; set; }
    }
}