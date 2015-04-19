using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Models
{
    public abstract class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool Active { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}