using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentingSystem.Infrastructure.Data.SeedDb
{
	internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            var data = new SeedData();

            builder.HasData(new Brand[] { data.AudiBrand, data.ToyotaBrand });
        }
    }
}

