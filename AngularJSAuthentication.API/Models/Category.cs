using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.API.Models
{
    public class Category : LookupBase
    {
        public Category() { }

        public Category(string code, string description, string username)
        {
            Code = code;
            Description = description;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;
        }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<LookupValue> LookupValues { get; set; } 
    }
}