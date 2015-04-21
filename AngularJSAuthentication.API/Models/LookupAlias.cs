using System;
using System.ComponentModel.DataAnnotations.Schema;
using AngularJSAuthentication.API.Dto;

namespace AngularJSAuthentication.API.Models
{
    public class LookupAlias : EntityBase
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

        public LookupAlias(LookupAliasDto lookupAliasDto, string username)
        {
            Name = lookupAliasDto.Name;
            LookupValueId = lookupAliasDto.LookupValueId;
            Active = lookupAliasDto.Active;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            CreatedBy = username;
            ModifiedBy = username;
        }
        
        public string Name { get; set; }

        public long LookupValueId { get; set; }

        [ForeignKey("LookupValueId")]
        public virtual LookupValue LookupValue { get; set; }
    }
}