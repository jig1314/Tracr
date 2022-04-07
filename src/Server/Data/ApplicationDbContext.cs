using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using Tracr.Server.Models;
using Tracr.Shared.Models;

namespace Tracr.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<ApplicationUserDetail> ApplicationUserDetails { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Mortage> Mortages { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Renter> Renters { get; set; }

        public DbSet<PropertyIncome> PropertyIncome { get; set; }

        #region Sql Statements

        public Task<List<PropertyIncome>> GetAllUserPropertyIncome(string idUser) =>
            PropertyIncome.FromSqlRaw(
            @"
                ;with cte as 
                (
	                select
		                Renters.Id,
		                Renters.PropertyId,
		                Renters.MonthlyRent, 
		                Renters.StartingMonth, 
		                Renters.EndingMonth
	                from Renters
	                inner join Properties on Properties.Id = Renters.PropertyId
	                where Properties.ApplicationUserId = {0}
	                union all
	                select 
		                Id,
		                PropertyId,
		                MonthlyRent,
		                dateadd(month, 1, StartingMonth),
		                EndingMonth
	                from cte 
	                where dateadd(month, 1, StartingMonth) < EndingMonth
                )
                select 
	                Id as RenterId,
	                PropertyId,
	                MonthlyRent as Income, 
	                StartingMonth as Month
                from cte", idUser)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();

        public Task<List<PropertyIncome>> GetPropertyIncome(int propertyId) =>
            PropertyIncome.FromSqlRaw(
            @"
                ;with cte as 
                (
	                select
		                Renters.Id,
		                Renters.PropertyId,
		                Renters.MonthlyRent, 
		                Renters.StartingMonth, 
		                Renters.EndingMonth
	                from Renters
	                where Renters.PropertyId = {0}
	                union all
	                select 
		                Id,
		                PropertyId,
		                MonthlyRent,
		                dateadd(month, 1, StartingMonth),
		                EndingMonth
	                from cte 
	                where dateadd(month, 1, StartingMonth) < EndingMonth
                )
                select 
	                Id as RenterId,
	                PropertyId,
	                MonthlyRent as Income, 
	                StartingMonth as Month
                from cte", propertyId)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();

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

            builder.Entity<Property>()
                .HasKey(p => p.Id)
                .HasName("PrimaryKey_PropertyId");

            builder.Entity<Property>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Properties)
                .IsRequired()
                .HasForeignKey(p => p.ApplicationUserId)
                .HasConstraintName("ForeignKey_Property_ApplicationUserId");

            builder.Entity<Property>().Property(p => p.NumBathrooms).HasPrecision(3, 1);

            builder.Entity<Mortage>()
                .HasKey(m => m.PropertyId)
                .HasName("PrimaryKey_Mortage_PropertyId");

            builder.Entity<Mortage>()
                .HasOne(m => m.Property)
                .WithOne(p => p.Mortage)
                .IsRequired()
                .HasForeignKey<Mortage>(m => m.PropertyId)
                .HasConstraintName("ForeignKey_Mortage_Property");

            builder.Entity<Mortage>().Property(m => m.Principal).HasPrecision(18, 2);
            builder.Entity<Mortage>().Property(m => m.MonthlyPayment).HasPrecision(18, 2);
            builder.Entity<Mortage>().Property(m => m.APR).HasPrecision(10, 3);

            builder.Entity<Address>()
                .HasKey(a => a.PropertyId)
                .HasName("PrimaryKey_Address_PropertyId");

            builder.Entity<Address>()
                .HasOne(a => a.Property)
                .WithOne(p => p.Address)
                .IsRequired()
                .HasForeignKey<Address>(a => a.PropertyId)
                .HasConstraintName("ForeignKey_Address_Property");

            builder.Entity<Renter>()
                .HasKey(r => r.Id)
                .HasName("PrimaryKey_RenterId");

            builder.Entity<Renter>()
                .HasOne(r => r.Property)
                .WithMany(p => p.Renters)
                .IsRequired()
                .HasForeignKey(r => r.PropertyId)
                .HasConstraintName("ForeignKey_Renter_Property");

            builder.Entity<Renter>().Property(r => r.MonthlyRent).HasPrecision(18, 2);

            builder.Entity<PropertyIncome>().HasNoKey();
        }

        #endregion

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                .HaveColumnType("date");
        }
    }

    /// <summary>
    /// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
    /// </summary>
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyConverter() : base(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d))
        { }
    }

    /// <summary>
    /// Compares <see cref="DateOnly" />.
    /// </summary>
    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyComparer() : base(
            (d1, d2) => d1 == d2 && d1.DayNumber == d2.DayNumber,
            d => d.GetHashCode())
        { }
    }
}