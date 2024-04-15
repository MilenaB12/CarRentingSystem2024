using System;
using CarRentingSystem.Core.Models.Admin;

namespace CarRentingSystem.Core.Contracts
{
	public interface IUserService
	{
		Task<string> UserFullName(string userId);

        Task<IEnumerable<UserServiceModel>> AllAsync();

        Task<bool> UserHasRentsAsync(string userId);
    }
}

