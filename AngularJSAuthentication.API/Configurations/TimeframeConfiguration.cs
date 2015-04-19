using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Configurations
{
    public class TimeframeConfiguration : EntityTypeConfiguration<Timeframe>
    {
        public TimeframeConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(i => i.Name)
                .IsRequired();

            Property(i => i.Description)
                .IsRequired();

            HasOptional(i => i.Parent)
                .WithMany(i => i.Children)
                .HasForeignKey(i => i.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}