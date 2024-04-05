using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static CarRentingSystem.Infrastructure.Constants.CustomClaims;

namespace CarRentingSystem.Infrastructure.Data.SeedDb
{
	public class SeedData
	{
        public IdentityUserClaim<string> DealerUserClaim { get; set; }

        public IdentityUserClaim<string> GuestUserClaim { get; set; }

        public IdentityUserClaim<string> AdminUserClaim { get; set; }

        public Category SedanCategory { get; set; }

        public Category LuxuryCategory { get; set; }

        public Brand AudiBrand { get; set; }

        public Brand ToyotaBrand { get; set; }

        public Car FirstCar { get; set; }

        public SeedData()
        {
            SeedUsersClaims();
            SeedCategories();
            SeedBrand();
            SeedCar();
        }

        private void SeedUsersClaims()
        {
            DealerUserClaim = new IdentityUserClaim<string>()
            {
                Id = 1,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Ivana Simeonova",
                UserId = "70d8118c-fbff-4bca-9e8d-addca4d36e62"
            };

            GuestUserClaim = new IdentityUserClaim<string>()
            {
                Id = 2,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Ivana Simeonova",
                UserId = "6da2c0d2-b759-429b-86a1-8d566966fe01"
            };

            AdminUserClaim = new IdentityUserClaim<string>()
            {
                Id = 3,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Ivan Ivanov",
                UserId = "6da2c0d2-b759-429b-86a1-8d566966fe01"
            };
        }

        private void SeedCategories()
        {

            SedanCategory = new Category()
            {
                Id = 5,
                Name = "Sedan"
            };

            LuxuryCategory = new Category()
            {
                Id = 6,
                Name = "Luxury"
            };
        }

        private void SeedBrand()
        {

            AudiBrand = new Brand()
            {
                Id = 3,
                Name = "Audi",
            };

            ToyotaBrand = new Brand()
            {
                Id = 4,
                Name = "Toyota",
            };
        }

        private void SeedCar()
        {
            FirstCar = new Car()
            {
                Id = 3,
                Color = "black",
                Description = "The car has no complaints",
                FuelType = Enums.FuelType.Diesel,
                GearType = Enums.GearType.Automatic,
                Price = 4500,
                ImageUrl = "https://i.ytimg.com/vi/gxaUwYHMqpE/maxresdefault.jpg",
                CategoryId = 6,
                DealerId = 1,
                BrandId = 3
            };
        }
    }  

}


