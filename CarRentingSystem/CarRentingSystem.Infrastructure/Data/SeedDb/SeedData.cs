using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace CarRentingSystem.Infrastructure.Data.SeedDb
{
	public class SeedData
	{
        public Category SedanCategory { get; set; }

        public Category LuxuryCategory { get; set; }

        public Brand AudiBrand { get; set; }

        public Brand ToyotaBrand { get; set; }

        public Car FirstCar { get; set; }

        public SeedData()
        {
            SeedCategories();
            SeedBrand();
            SeedCar();
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


