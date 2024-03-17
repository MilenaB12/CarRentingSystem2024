using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class DealerService : IDealerService
    {
        private readonly IRepository repository;

        public DealerService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Dealer()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userID)
        {
            return await repository.AllReadOnly<Dealer>()
                .AnyAsync(d => d.UserId == userID);
        }

        public async Task<int?> GetDealerIdAsync(string userId)
        {
            return (await repository.AllReadOnly<Dealer>()
                .FirstOrDefaultAsync(d => d.UserId == userId))?.Id;
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c => c.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Dealer>()
                .AnyAsync(d => d.PhoneNumber == phoneNumber);
        }
    }
}

