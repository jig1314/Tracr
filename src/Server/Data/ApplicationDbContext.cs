using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tracr.Server.Models;

namespace Tracr.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<ApplicationUserDetail> ApplicationUserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserDetail>()
                .HasKey(e => e.ApplicationUserId)
                .HasName("PrimaryKey_ApplicationUserId");

            builder.Entity<ApplicationUserDetail>()
                .HasOne(e => e.ApplicationUser)
                .WithOne(u => u.ApplicationUserDetail)
                .IsRequired()
                .HasForeignKey<ApplicationUserDetail>(e => e.ApplicationUserId)
                .HasConstraintName("ForeignKey_User_UserDetail");
        }
    }
}