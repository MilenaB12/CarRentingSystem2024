using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data.Common;

namespace CarRentingSystem.Tests
{
	public class StatisticsTests : UnitTestsBase
	{
		private IStatisticService statisticService;

		[OneTimeSetUp]
		public void SetUp()
		{
			var repo = new Repository(data);
			statisticService = new StatisticService(repo);
        }

		[Test]
		public async Task TotalAsyncShouldReturnCorrectResult()
		{
			var result = await statisticService.TotalAsync();

			Assert.IsNotNull(result);

			var carsCount = data.Cars.Count();
			Assert.That(result.TotalCars, Is.EqualTo(carsCount));

			var rentCarsCount = data.Cars.Where(c => c.RenterId != null).Count();
			Assert.That(result.TotalRents, Is.EqualTo(rentCarsCount));

        }
	}
}

