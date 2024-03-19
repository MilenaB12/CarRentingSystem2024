﻿using System;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.Home;

namespace CarRentingSystem.Core.Contracts
{
	public interface ICarService
	{
		Task<IEnumerable<CarIndexServiceModel>> LastCarsAsync();

        Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync();

        Task<IEnumerable<CarBrandServiceModel>> AllBrandsAsync();

        Task<bool>CategoryExistsAsync(int categoryId);

        Task<bool> BrandExistsAsync(int brandId);

        Task<int> CreateAsync(CarFormModel model, int dealerId);

    }
}

