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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<LookupValue> LookupValues { get; set; }
        public DbSet<LookupAlias> LookupAliases { get; set; }
        public DbSet<Timeframe> Timeframes { get; set; }
    }
}