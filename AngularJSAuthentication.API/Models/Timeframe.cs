using System;
using System.Collections.Generic;

namespace AngularJSAuthentication.API.Models
{
    public class Timeframe : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public virtual List<Timeframe> ChiTimeframes { get; set; }
    }
}