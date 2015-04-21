using System.Data.Entity;
using AngularJSAuthentication.API.Configurations;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbContexts
{
    public class LookupContext : DbContext
    {
        public LookupContext()
            : base("LookupContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TimeframeConfiguration());

            modelBuilder.Entity<TemplateTask>()
                           .HasKey(cp => new { cp.TemplateId, cp.TaskId });

            modelBuilder.Entity<ActivityTemplate>()
                        .HasMany(c => c.TemplateTasks)
                        .WithRequired()
                        .HasForeignKey(cp => cp.TemplateId);

            modelBuilder.Entity<ActivityTask>()
                        .HasMany(p => p.TemplateTasks)
                        .WithRequired()
                        .HasForeignKey(cp => cp.TaskId); 
 
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<LookupValue> LookupValues { get; set; }
        public DbSet<LookupAlias> LookupAliases { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
        public DbSet<ActivityTask> ActivityTasks { get; set; }
    }
}