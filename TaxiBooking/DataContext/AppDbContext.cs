using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TaxiBooking.Models;

namespace TaxiBooking.DataContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<TaxiType> TaxiTypes { get; set; }
        public DbSet<Taxi> Taxis { get; set; }
        public DbSet<JourneyLog> Journey { get;set; }
        public DbSet<DriverState> DriverState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
                .HasIndex(e => e.PhoneNumber)
                .IsUnique();
        }

        public override int SaveChanges()
        {
            var journeyEntries = ChangeTracker.Entries<JourneyLog>();

            foreach (var entry in journeyEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }

            var taxiEntries = ChangeTracker.Entries<Taxi>();

            foreach (var entry in taxiEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }

            var driverStateEntries = ChangeTracker.Entries<DriverState>();
            foreach (var entry in driverStateEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }

            var userEntries = ChangeTracker.Entries<AppUser>();

            foreach (var entry in userEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }


            return base.SaveChanges();
        }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();
        }
    }
}
