using CarRentalApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rental>()
                .HasOne(r => r.PickupLocation).WithMany(l => l.PickupRentals)
                .HasForeignKey(r => r.PickupLocationId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rental>()
                .HasOne(r => r.ReturnLocation).WithMany(l => l.ReturnRentals)
                .HasForeignKey(r => r.ReturnLocationId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rental>()
                .HasOne(r => r.Car).WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rental>()
                .HasOne(r => r.User).WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.Car).WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CarId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.User).WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CarCategory>().HasData(
                new CarCategory { Id=1, Name="Sedan", Description="Masini clasice sedan" },
                new CarCategory { Id=2, Name="SUV", Description="Sport Utility Vehicle" },
                new CarCategory { Id=3, Name="Sport", Description="Masini de performanta" },
                new CarCategory { Id=4, Name="Electric", Description="Vehicule electrice" },
                new CarCategory { Id=5, Name="Minivan", Description="Masini spatioase pentru familii" }
            );

            builder.Entity<Location>().HasData(
                new Location { Id=1, Name="Centru Craiova", Address="Calea Unirii 10", City="Craiova" },
                new Location { Id=2, Name="Aeroport Craiova", Address="Str. Aeroportului 1", City="Craiova" },
                new Location { Id=3, Name="Gara Craiova", Address="Str. Garii 5", City="Craiova" },
                new Location { Id=4, Name="Centru Bucuresti", Address="Calea Victoriei 100", City="Bucuresti" }
            );
        }
    }
}
