using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Models
{
    public class CategoryType
    {
        public CategoryType() { }

        public CategoryType(string name)
        {
            Name = name;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; } 
    }
}