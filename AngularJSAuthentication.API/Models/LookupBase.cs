using System;

namespace AngularJSAuthentication.API.Models
{
    public abstract class LookupBase
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}