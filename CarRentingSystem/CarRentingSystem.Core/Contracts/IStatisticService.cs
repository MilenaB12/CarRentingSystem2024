using System;
using CarRentingSystem.Core.Models.Statistics;

namespace CarRentingSystem.Core.Contracts
{
	public interface IStatisticService
	{
		Task<StatisticsModel> TotalAsync();
	}
}

