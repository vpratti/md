using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Configurations
{
    public class ActivityTaskConfiguration : EntityTypeConfiguration<ActivityTask>
    {
        public ActivityTaskConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.Name)
                .IsRequired();

          
        }
    }
}