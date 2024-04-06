using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Admin;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class RentService : IRentService
    {
        private readonly IRepository repository;

        public RentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<RentServiceModel>> AllRentAsync()
        {
            return await repository.AllReadOnly<Car>()
                .Where(C => C.RenterId != null) 
                .Include(c => c.Dealer)
                .Include(c => c.Renter)
                .Select(c => new RentServiceModel()
                {
                    CarBrand = c.Brand.Name,
                    CarImageUrl = c.ImageUrl,
                    DealerEmail = c.Dealer.User.Email,
                    DealerFullName = $"{c.Dealer.User.FirstName} {c.Dealer.User.LastName}",
                    RenterEmail = c.Renter != null ? c.Renter.Email : string.Empty,
                    RenterFullName = c.Renter != null ? $"{c.Renter.FirstName} {c.Renter.LastName}" : string.Empty
                })
                .ToListAsync();
        }
    }
}

