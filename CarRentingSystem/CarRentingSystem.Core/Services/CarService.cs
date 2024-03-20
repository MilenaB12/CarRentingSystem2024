using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Enumerations;
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
                .Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Color = c.Color,
                    Brand = c.Brand.Name,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    IsRented = c.RenterId != null
                }).ToListAsync();

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
                    Model = b.Model
                }).ToListAsync();
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
    }
}

