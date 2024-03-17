using System;
using CarRentingSystem.Core.Contracts;
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

        public async Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => new CarCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
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
                BrandId = model.BrandId,
            };

            await repository.AddAsync(car);
            await repository.SaveChangesAsync();

            return car.Id;
        }

        public async Task<IEnumerable<CarIndexServiceModel>> LastCarsAsync()
        {
            return await repository
                .AllReadOnly<Infrastructure.Data.Models.Car>()
                .OrderByDescending(c => c.Id)
                .Take(3)
                .Select(c => new CarIndexServiceModel()
                {
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    Brand = c.Brand.Name
                }).ToListAsync();
        }
    }
}

