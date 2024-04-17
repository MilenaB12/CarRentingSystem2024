using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Tests
{
    [TestFixture]
    public class CarServiceTests : UnitTestsBase
    {
        private IUserService userService;

        private ICarService carService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var repo = new Repository(data);
            userService = new UserService(repo);
            carService = new CarService(repo);
        }

        [Test]
        public async Task AllCategoriesNamesAsyncShouldReturnCorrectNames()
        {
            var result = await carService.AllCategoriesNamesAsync();

            var categories = data.Categories;

            Assert.That(result.Count(), Is.EqualTo(categories.Count()));

            var categoryNames = categories.Select(c => c.Name);

            Assert.That(categoryNames.Contains(result.FirstOrDefault()));
        }


        [Test]
        public async Task AllCarsByUserIdAsyncShouldReturnCorrectCars()
        {
            var renterId = Renter.Id;

            var result = await carService.AllCarsByUserIdAsync(renterId);

            var cars = data.Cars.Where(c => c.RenterId == renterId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(cars.Count()));
        }

        [Test]
        public async Task ExistsAsyncShouldReturnCorrectResult()
        {
            var car = data.Cars.FirstOrDefault(c => c.RenterId != null);

            var result = await carService.ExistsAsync(car.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CarDetailsByIdAsyncShouldReturnCorrectData()
        {
            var car = data.Cars.FirstOrDefault(c => c.RenterId != null);

            var result = await carService.CarDetailsByIdAsync(car.Id);

            var mycar = data.Cars.Find(car.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(mycar.Id));
            Assert.That(result.Brand, Is.EqualTo(mycar.Brand.Name));
        }

        [Test]
        public async Task AllCategoriesAsyncShouldReturnCorrectCategories()
        {
            var result = await carService.AllCategoriesAsync();

            var categories = data.Categories;

            var categoryNames = categories.Select(c => c.Name);

            Assert.That(result.Count(), Is.EqualTo(categories.Count()));
            Assert.That(categoryNames.Contains(result.FirstOrDefault().Name));

        }

        [Test]
        public async Task AllBrandsAsyncShouldReturnCorrectBrands()
        {
            var result = await carService.AllBrandsAsync();

            var brands = data.Brands;

            var brandNames = brands.Select(c => c.Name);

            Assert.That(result.Count(), Is.EqualTo(brands.Count()));
            Assert.That(brandNames.Contains(result.FirstOrDefault().Name));

        }

        [Test]
        public async Task AllLocationsAsyncShouldReturnCorrectLocations()
        {
            var result = await carService.AllLocationsAsync();

            var locations = data.Locations;

            var locationNames = locations.Select(c => c.Name);

            Assert.That(result.Count(), Is.EqualTo(locations.Count()));
            Assert.That(locationNames.Contains(result.FirstOrDefault().Name));

        }

        [Test]
        public async Task CreateAsyncShouldCreateCar()
        {
            var firstCarsCount = data.Cars.Count();

            var carFormModel = new CarFormModel()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
            };

            var carId = await carService.CreateAsync(carFormModel, Dealer.Id);

            var carsCount = data.Cars.Count();

            Assert.That(carsCount, Is.EqualTo(firstCarsCount + 1));

            var car = data.Cars.Find(carId);

            Assert.That(car.Color, Is.EqualTo(carFormModel.Color));
        }

        [Test]
        public async Task HasDealerWithIdAsyncShouldReturnTrue()
        {
            var car = data.Cars.FirstOrDefault(c => c.RenterId != null);
            var user = data.Cars.FirstOrDefault(c => c.RenterId != null);

            var carId = car.Id;
            var userId = user.Dealer.User.Id;

            var result = await carService.HasDealerWithIdAsync(carId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task EditAsyncShouldEditCorrect()
        {
            var car = new Car()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
                DealerId = Dealer.Id,
                IsApproved = true,
                Renter = Renter
            };

            await data.Cars.AddAsync(car);
            await data.SaveChangesAsync();

            var changedColor = "purple";

            var carFormModel = new CarFormModel()
            {
                Color = changedColor,
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
            };

            await carService.EditAsync(car.Id, carFormModel);

            var newCar = await data.Cars.FindAsync(car.Id);

            Assert.IsNotNull(newCar);
            Assert.That(newCar.Color, Is.EqualTo(changedColor));
            Assert.That(newCar.Brand, Is.EqualTo(car.Brand));
        }

        [Test]
        public async Task DeleteAsyncShouldeleteCorrect()
        {
            var car = new Car()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
                DealerId = Dealer.Id,
                IsApproved = true,
                Renter = Renter
            };

            await data.Cars.AddAsync(car);
            await data.SaveChangesAsync();

            var firstCarCount = data.Cars.Count();

            await carService.DeleteAsync(car.Id);

            var carsCountAfterDelete = data.Cars.Count();

            var carWithId = await data.Cars.FindAsync(car.Id);

            Assert.That(carsCountAfterDelete, Is.EqualTo(firstCarCount - 1));
            Assert.IsNull(carWithId);
        }

        [Test]
        public async Task IsRentedAsyncShouldReturnTrue()
        {
            var car = data.Cars.FirstOrDefault(c => c.RenterId != null);

            var carId = car.Id;

            var result = await carService.IsRentedAsync(carId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task RentAsyncShouldRentCar()
        {
            var car = new Car()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
                DealerId = Dealer.Id,
                IsApproved = true,
                Renter = Renter
            };

            await data.Cars.AddAsync(car);
            await data.SaveChangesAsync();

            var renterId = Renter.Id;

            await carService.RentAsync(car.Id, renterId);

            var newCar = data.Cars.Find(car.Id);

            Assert.IsNotNull(newCar);
            Assert.That(renterId, Is.EqualTo(car.RenterId));
        }

        [Test]
        public async Task LeaveAsyncShouldLeaveCar()
        {
            var car = new Car()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
                DealerId = Dealer.Id,
                IsApproved = true,
                Renter = Renter
            };

            await data.Cars.AddAsync(car);
            await data.SaveChangesAsync();

            var renterId = Renter.Id;

            await carService.LeaveAsync(car.Id, renterId);

            var newCar = await data.Cars.FindAsync(car.Id);

            Assert.IsNull(car.RenterId);
            Assert.IsNotNull(newCar);
            Assert.IsNull(newCar.RenterId);

        }

        [Test]
        public async Task LastCarsAsyncShouldReturnCarsCorrectly()
        {
            var result = await carService.LastCarsAsync();

            var cars = await data.Cars
                .Where(c => c.IsApproved)
                .OrderByDescending(c => c.Id)
                .Take(3).ToListAsync();

            var firstCar = cars.FirstOrDefault();

            var firstResultCar = result.FirstOrDefault();

            Assert.That(result.Count(), Is.EqualTo(cars.Count()));
            Assert.That(firstResultCar.Id, Is.EqualTo(firstCar.Id));

        }

        [Test]
        public async Task CategoryExistsAsyncShouldReturnTrue()
        {
            var category = data.Cars.FirstOrDefault();

            var categoryId = category.Id;

            var result = await carService.CategoryExistsAsync(categoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task BrandExistsAsync()
        {
            var brand = data.Cars.FirstOrDefault();

            var brandId = brand.Id;

            var result = await carService.BrandExistsAsync(brandId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task LocationExistsAsync()
        {
            var location = data.Cars.FirstOrDefault();

            var locationId = location.Id;

            var result = await carService.LocationExistsAsync(locationId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetCarFormModelByIdAsyncShouldReturnCorrectForm()
        {
            var car = new Car()
            {
                Color = "blue",
                Price = 1000,
                BrandId = 1,
                CategoryId = 1,
                Description = "The best car ever!",
                LocationId = 1,
                FuelType = Infrastructure.Enums.FuelType.Gasoline,
                GearType = Infrastructure.Enums.GearType.Manual,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Opel_Corsa_B_WorldCup_Facelift.JPG/640px-Opel_Corsa_B_WorldCup_Facelift.JPG",
                Year = 1997,
                DealerId = Dealer.Id,
                IsApproved = true,
                Renter = Renter
            };


            await data.Cars.AddAsync(car);
            await data.SaveChangesAsync();

            var newCar = await carService.GetCarFormModelByIdAsync(car.Id);

            Assert.That(newCar.ImageUrl, Is.EqualTo(car.ImageUrl));
        }


        [Test]
        public async Task IsRentedByUserWithIdAsyncShouldReturnTrue()
        {
            var car = data.Cars.FirstOrDefault(c => c.RenterId != null);
            var user = data.Cars.FirstOrDefault(c => c.RenterId != null);

            var carId = car.Id;
            var userId = user.Renter.Id;

            var result = await carService.IsRentedByUserWithIdAsync(carId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetUnApprovedAsyncShouldReturnCorrectCars()
        {

            var result = await carService.GetUnApprovedAsync();
            var approvedCars = await data.Cars.Where(c => c.IsApproved == false).ToListAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(approvedCars.Count()));
        }

    }
}

