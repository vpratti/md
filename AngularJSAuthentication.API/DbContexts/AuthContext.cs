using System.Data.Entity;
using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AngularJSAuthentication.API.DbContexts
{
    public class AuthContext : IdentityDbContext<VirtualClarityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
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