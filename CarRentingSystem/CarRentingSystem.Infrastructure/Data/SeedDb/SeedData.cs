using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static CarRentingSystem.Infrastructure.Constants.CustomClaims;

namespace CarRentingSystem.Infrastructure.Data.SeedDb
{
	public class SeedData
	{
        public ApplicationUser DealerUser { get; set; }

        public ApplicationUser GuestUser { get; set; }

        public IdentityUserClaim<string> DealerUserClaim { get; set; }

        public IdentityUserClaim<string> GuestUserClaim { get; set; }

        public Dealer Dealer { get; set; }

        public Category SedanCategory { get; set; }

        public Category LuxuryCategory { get; set; }

        public Brand AudiBrand { get; set; }

        public Brand ToyotaBrand { get; set; }

        public Car FirstCar { get; set; }

        public Car SecondCar { get; set; }


        public SeedData()
        {
            SeedUsers();
            SeedUsersClaims();
            SeedDealer();
            SeedCategories();
            SeedBrand();
            SeedCar();
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            DealerUser = new ApplicationUser()
            {
                Id = "a56ec564-782b-6351-da53-81a4b53acaf2",
                UserName = "dealer@abv",
                NormalizedUserName = "dealer@abv",
                Email = "dealer@abv",
                NormalizedEmail = "dealer@abv",
                FirstName = "Mitko",
                LastName = "Dimitrov"
            };

            DealerUser.PasswordHash =
                 hasher.HashPassword(DealerUser, "Mitko.1");

            GuestUser = new ApplicationUser()
            {
                Id = "24a2453b-bfea-3abe-4b2a-beabf3525a21",
                UserName = "Simona@abv",
                NormalizedUserName = "Simona@abv",
                Email = "Simona@abv",
                NormalizedEmail = "Simona@abv",
                FirstName = "Simona",
                LastName = "Hristova"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(DealerUser, "Simona.1");
        }

        private void SeedUsersClaims()
        {
            DealerUserClaim = new IdentityUserClaim<string>()
            {
                Id = 4,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Mitko Dimitrov",
                UserId = "a56ec564-782b-6351-da53-81a4b53acaf2"
            };

            GuestUserClaim = new IdentityUserClaim<string>()
            {
                Id = 5,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Simona Hristova",
                UserId = "24a2453b-bfea-3abe-4b2a-beabf3525a21"
            };
        }


        private void SeedDealer()
        {
            Dealer = new Dealer()
            {
                Id = 2,
                PhoneNumber = "+359676767676",
                UserId = DealerUser.Id
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

            SecondCar = new Car()
            {
                Id = 4,
                Color = "grey",
                Description = "Whether you're headed out of town for a vacation, need a vehicle for business in a new city, have your current car in the shop, or are looking to experience an extended test drive before purchase, you can rely on a Toyota car rental.",
                FuelType = Enums.FuelType.Electric,
                GearType = Enums.GearType.Automatic,
                Price = 4200,
                ImageUrl = "https://mobistatic4.focus.bg/mobile/photosorg/821/1/big//11690282183094821_4k.jpg",
                CategoryId = SedanCategory.Id,
                DealerId = Dealer.Id,
                BrandId = ToyotaBrand.Id
            };
        }
    }  

}


