using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Models
{
    public class LookupAlias : LookupBase
    {
        public LookupAlias()
        {
        }

        public LookupAlias(string name, bool active, long lookupValueId)
        {
            Name = name;
            LookupValueId = lookupValueId;
            Active = active;
        }

        public LookupAlias(string name, bool active, long lookupValueId, string username)
        {
            Name = name;
            LookupValueId = lookupValueId;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = active;
        }
        
        public string Name { get; set; }

        public long LookupValueId { get; set; }

        [ForeignKey("LookupValueId")]
        public virtual LookupValue LookupValue { get; set; }
    }
}