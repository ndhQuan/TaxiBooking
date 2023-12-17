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

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Entity.CreatedAt = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Entity.LastUpdatedAt = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();
        }
    }
}
