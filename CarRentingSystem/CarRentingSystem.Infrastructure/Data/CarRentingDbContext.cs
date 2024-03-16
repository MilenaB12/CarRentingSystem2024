using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Infrastructure.Data;

public class CarRentingDbContext : IdentityDbContext
{
    public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Reservation>()
        .HasKey(x => new { x.CarId, x.DealerId });
        builder.Entity<Car>()
            .Property(c => c.Price)
            .HasPrecision(18, 2);

        builder.Entity<Car>()
.HasOne(c => c.Category)
.WithMany(ct => ct.Cars)
.HasForeignKey(c => c.CategoryId)
.OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Car>()
            .HasOne(c => c.Dealer)
            .WithMany(d => d.Cars)
            .HasForeignKey(c => c.DealerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Reservation>()
    .HasOne(c => c.Dealer)
    .WithMany(d => d.Reservations)
    .HasForeignKey(c => c.DealerId)
    .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }

    public DbSet<Brand> Brands { get; set; } = null!;

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Dealer> Dealers { get; set; } = null!;

    public DbSet<Reservation> Reservations { get; set; } = null!;
}

