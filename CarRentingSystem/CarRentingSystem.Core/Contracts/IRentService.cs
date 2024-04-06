using System;
using CarRentingSystem.Core.Models.Admin;

namespace CarRentingSystem.Core.Contracts
{
	public interface IRentService
	{
		Task<IEnumerable<RentServiceModel>> AllRentAsync();
	}
}

