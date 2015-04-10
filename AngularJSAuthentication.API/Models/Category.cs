using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularJSAuthentication.API.Models
{
    public class Category : LookupBase
    {
        public Category() { }

        public Category(string code, string description, long categoryTypeId)
        {
            Code = code;
            Description = description;
            CategoryTypeId = categoryTypeId;
        }

        public Category(string code, string description, long categoryTypeId, long id)
        {
            Code = code;
            Description = description;
            CategoryTypeId = categoryTypeId;
            Id = id;
        }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public long CategoryTypeId { get; set; }

        [ForeignKey("CategoryTypeId")]
        public virtual CategoryType Type { get; set; }
    }
}