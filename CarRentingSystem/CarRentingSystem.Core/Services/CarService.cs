using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Enumerations;
using CarRentingSystem.Core.Exceptions;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.Home;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<CarQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, CarSorting sorting = CarSorting.Newest, int currentPage = 1, int carsPerPage = 1)
        {
            var carsToshow = repository.AllReadOnly<Car>();

            if (category != null)
            {
                carsToshow = carsToshow
                    .Where(c => c.Category.Name == category);
            }

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();

                carsToshow = carsToshow
                    .Where(h => h.Color.ToLower().Contains(normalizedSearchTerm));
            }

            carsToshow = sorting switch
            {
                CarSorting.Price => carsToshow
                .OrderBy(c => c.Price),
                CarSorting.NotRented => carsToshow
                    .OrderBy(c => c.RenterId == null)
                    .ThenByDescending(c => c.Id),
                _ => carsToshow
                    .OrderByDescending(c => c.Id)
            };

            var cars = await carsToshow
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .ProjectToCarServiceModel()
                .ToListAsync();

            int totalCars = await carsToshow.CountAsync();

            return new CarQueryServiceModel()
            {
                Cars = cars,
                TotalCarsCount = totalCars
            };
        }

        public async Task<IEnumerable<CarBrandServiceModel>> AllBrandsAsync()
        {
            return await repository.AllReadOnly<Brand>()
                .Select(b => new CarBrandServiceModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToListAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> AllCarsByDealerIdAsync(int dealerId)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.DealerId == dealerId)
                .ProjectToCarServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> AllCarsByUserId(string userId)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.RenterId == userId)
                .ProjectToCarServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => new CarCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> BrandExistsAsync(int brandId)
        {
            return await repository.AllReadOnly<Brand>()
                .AnyAsync(b => b.Id == brandId);
        }

        public async Task<CarDetailsServiceModel> CarDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsServiceModel()
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Dealer = new Models.Dealer.DealerServiceModel()
                    {
                        Email = c.Dealer.User.Email,
                        PhoneNumber = c.Dealer.PhoneNumber
                    },
                    Category = c.Category.Name,
                    Brand = c.Brand.Name,
                    Color = c.Color,
                    Description = c.Description,
                    FuelType = c.FuelType.ToString(),
                    GearType = c.GearType.ToString(),
                    Price = c.Price,
                    Year = c.Year,
                    IsRented = c.RenterId != null!
                }).FirstAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateAsync(CarFormModel model, int dealerId)
        {
            Car car = new Car()
            {
                Color = model.Color,
                DealerId = dealerId,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Year = model.Year,
                FuelType = model.FuelType,
                GearType = model.GearType,
                BrandId = model.BrandId
            };

            await repository.AddAsync(car);
            await repository.SaveChangesAsync();

            return car.Id;
        }

        public async Task DeleteAsync(int carId)
        {
            await repository.DeleteAsync<Car>(carId);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(int carId, CarFormModel model)
        {
            var car = await repository.GetByIdAsync<Car>(carId);

            if (car != null)
            {
                car.Color = model.Color;
                car.CategoryId = model.CategoryId;
                car.Description = model.Description;
                car.ImageUrl = model.ImageUrl;
                car.Price = model.Price;
                car.FuelType = model.FuelType;
                car.GearType = model.GearType;
                car.BrandId = model.BrandId;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c => c.Id == id);
        }

        public async Task<CarFormModel?> GetCarFormModelByIdAsync(int id)
        {
            var car = await repository.AllReadOnly<Car>()
                .Where(c => c.Id == id)
                .Select(c => new CarFormModel()
                {
                    Color = c.Color,
                    CategoryId = c.CategoryId,
                    BrandId = c.BrandId,
                    Description = c.Description,
                    FuelType = c.FuelType,
                    GearType = c.GearType,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    Year = c.Year
                }).FirstOrDefaultAsync();

            if (car != null)
            {
                car.Categories = await AllCategoriesAsync();
                car.Brands = await AllBrandsAsync();
            }

            return car;     
        }

        public async Task<bool> HasDealerWithIdAsync(int carId, string userId)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c => c.Id == carId && c.Dealer.UserId == userId);
        }

        public async Task<bool> IsRentedAsync(int carId)
        {
            bool result = false;
            var car = await repository.GetByIdAsync<Car>(carId);

            if (car != null)
            {
                result = car.RenterId != null;
            }

            return result;
        }

        public async Task<bool> IsRentedByIUserWithIdAsync(int carId, string userId)
        {
            bool result = false;
            var house = await repository.GetByIdAsync<Car>(carId);

            if (house != null)
            {
                result = house.RenterId == userId;
            }

            return result;
        }

        public async Task<IEnumerable<CarIndexServiceModel>> LastCarsAsync()
        {
            return await repository
                .AllReadOnly<Car>()
                .Take(3)
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexServiceModel()
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Brand = c.Brand.Name
                }).ToListAsync();
        }

        public async Task LeaveAsync(int carId, string userId)
        {
            var car = await repository.GetByIdAsync<Car>(carId);

            if (car != null)
            {
                if (car.RenterId != userId)
                {
                    throw new UnauthorizedActionException("The user is not the renter");
                }

                car.RenterId = null;
                await repository.SaveChangesAsync();
            }
        }

        public async Task RentAsync(int id, string userId)
        {
            var car = await repository.GetByIdAsync<Car>(id);

            if (car != null)
            {
                car.RenterId = userId;
                await repository.SaveChangesAsync();
            }
        }
    }
}

