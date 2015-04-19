using System.Data.Entity;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API
{
    public class LookupContext : DbContext
    {
        public LookupContext()
            : base("LookupContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<LookupValue> LookupValues { get; set; }
        public DbSet<LookupAlias> LookupAliases { get; set; } 
    }
}