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

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();
        }
    }
}
