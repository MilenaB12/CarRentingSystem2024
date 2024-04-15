using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data.Common;

namespace CarRentingSystem.Tests
{
	public class RentServiceTests : UnitTestsBase
	{
		private IRentService rentService;

        [OneTimeSetUp]
        public void SetUp()
		{
            var repo = new Repository(data);
            rentService = new RentService(repo);
        }

        [Test]
        public async Task AllAsyncShouldReturnCorrect()
        {
            var result = await rentService.AllRentAsync();

            Assert.IsNotNull(result);

            var rentedCars = data.Cars
                .Where(c => c.RenterId != null);

            Assert.That(result.ToList().Count(), Is.EqualTo(rentedCars.Count()));

            var car = result.ToList()
                .Find(c => c.CarBrand == rentedCars.FirstOrDefault().Brand.Name);

            Assert.IsNotNull(car);
            Assert.That(car.RenterEmail, Is.EqualTo(Renter.Email));
            Assert.That(car.RenterFullName, Is.EqualTo(Renter.FirstName + " " + Renter.LastName));
            Assert.That(car.DealerEmail, Is.EqualTo(Dealer.User.Email));
            Assert.That(car.DealerFullName, Is.EqualTo(Dealer.User.FirstName + " " + Dealer.User.LastName));
        }
    }
}

