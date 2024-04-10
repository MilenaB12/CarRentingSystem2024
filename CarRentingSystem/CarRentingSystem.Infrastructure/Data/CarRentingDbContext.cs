using CarRentingSystem.Infrastructure.Data.Models;
using CarRentingSystem.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Infrastructure.Data;

public class CarRentingDbContext : IdentityDbContext<ApplicationUser>
{
    public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //builder.Entity<Car>()
        //    .HasKey(x => new { x.BrandId, x.CategoryId, x.DealerId, x.LocationId});

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

        builder.Entity<Car>()
.HasOne(c => c.Brand)
.WithMany(ct => ct.Cars)
.HasForeignKey(c => c.BrandId)
.OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Car>()
    .HasOne(c => c.Location)
    .WithMany(d => d.Cars)
    .HasForeignKey(c => c.LocationId)
    .OnDelete(DeleteBehavior.Restrict);


        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new DealerConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new LocationConfiguration());
        builder.ApplyConfiguration(new CarConfiguration());
        builder.ApplyConfiguration(new UserClaimsConfiguration());

        base.OnModelCreating(builder);
    }

    public DbSet<Brand> Brands { get; set; } = null!;

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Dealer> Dealers { get; set; } = null!;

    public DbSet<Location> Locations { get; set; } = null!;

}

