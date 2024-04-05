using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.SeedDb
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            var data = new SeedData();

            builder.HasData(new Car[] { data.FirstCar});
        }
    }
}
