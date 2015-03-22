using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AngularJSAuthentication.API
{
    public class AuthContext : IdentityDbContext<VirtualClarityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            //todo will have to move to api call due to migration
            //Database.SetInitializer(new AuthContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
               .ToTable("Users");
            modelBuilder.Entity<VirtualClarityUser>()
                .ToTable("Users");
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}