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

        Task<IEnumerable<CarServiceModel>> AllCarsByDealerIdAsync(int dealerId);

        Task<IEnumerable<CarServiceModel>> AllCarsByUserId(string userId);

        Task<bool> ExistsAsync(int id);

        Task<CarDetailsServiceModel> CarDetailsByIdAsync(int id);

        Task EditAsync(int carId, CarFormModel model);

        Task<bool> HasDealerWithIdAsync(int carId, string userId);

        Task<CarFormModel?> GetCarFormModelByIdAsync(int id);

        Task DeleteAsync(int carId);

        Task<bool> IsRentedAsync(int carId);

        Task<bool> IsRentedByUserWithIdAsync(int carId, string userId);

        Task RentAsync(int id, string userId);

        Task LeaveAsync(int carId, string userId);

        Task<IEnumerable<CarServiceModel>> GetApprovedAsync();

        Task ApproveCarAsync(int carId);

    }
}

