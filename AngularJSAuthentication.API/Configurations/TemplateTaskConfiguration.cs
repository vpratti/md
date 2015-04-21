using System.Data.Entity.ModelConfiguration;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Configurations
{
    public class TemplateTaskConfiguration : EntityTypeConfiguration<TemplateTask>
    {
        public TemplateTaskConfiguration()
        {
            HasKey(i => new { i.TemplateId, i.TaskId });

        }
    }
}