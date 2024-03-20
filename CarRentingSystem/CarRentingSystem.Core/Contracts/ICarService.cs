using System;
using CarRentingSystem.Core.Enumerations;
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

        Task<CarQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            CarSorting sorting = CarSorting.Newest,
            int currentPage = 1,
            int carsPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();

    }
}

