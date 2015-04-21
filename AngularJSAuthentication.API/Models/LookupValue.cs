using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AngularJSAuthentication.API.Dto;

namespace AngularJSAuthentication.API.Models
{
    public class LookupValue : EntityBase
    {
        public LookupValue() { }


        public LookupValue(string name, bool active, long categoryId)
        {
            Name = name;
            CategoryId = categoryId;
            Active = active;
        }

        public LookupValue(string name, bool active, long categoryId, string username)
        {
            Name = name;
            CategoryId = categoryId;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = active;
        }

        public LookupValue(LookupValueDto lookupValueDto, string username)
        {
            Name = lookupValueDto.Name;
            CategoryId = lookupValueDto.CategoryId;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = lookupValueDto.Active;
        }
        
        public string Name { get; set; }

        public virtual List<LookupAlias> LookupAliases { get; set; }

        public long CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}