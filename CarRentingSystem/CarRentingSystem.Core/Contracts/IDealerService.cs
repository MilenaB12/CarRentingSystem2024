using System;
namespace CarRentingSystem.Core.Contracts
{
	public interface IDealerService
	{
		Task<bool> ExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasRentsAsync(string userId);

        Task CreateAsync(string userId, string phoneNumber);
    }
}

