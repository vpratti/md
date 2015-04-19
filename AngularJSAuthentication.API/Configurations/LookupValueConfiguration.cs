using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Configurations
{
    public class LookupValueConfiguration : EntityTypeConfiguration<LookupValue>
    {
        public LookupValueConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.Name)
                .IsRequired();

            HasOptional(i => i.Parent)
                .WithMany(i => i.Children)
                .HasForeignKey(i => i.ParentId);
        }
    }
}