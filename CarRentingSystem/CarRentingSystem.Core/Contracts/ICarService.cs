using System;
using CarRentingSystem.Core.Models.Home;

namespace CarRentingSystem.Core.Contracts
{
	public interface ICarService
	{
		Task<IEnumerable<CarIndexServiceModel>> LastCars();
	}
}

