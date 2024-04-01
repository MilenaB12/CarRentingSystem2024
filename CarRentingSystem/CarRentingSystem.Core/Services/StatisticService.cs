using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Statistics;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;

        public StatisticService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<StatisticsModel> TotalAsync()
        {
            int totalCars = await repository.AllReadOnly<Car>()
                .CountAsync();

            int totalRents = await repository.AllReadOnly<Car>()
                .Where(c => c.RenterId != null)
                .CountAsync();

            return new StatisticsModel()
            {
                TotalCars = totalCars,
                TotalRents = totalRents
            };
        }
    }
}

