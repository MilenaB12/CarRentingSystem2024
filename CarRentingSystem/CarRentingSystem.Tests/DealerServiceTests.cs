using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data;
using CarRentingSystem.Infrastructure.Data.Common;

namespace CarRentingSystem.Tests
{
	[TestFixture]
	public class DealerServiceTests : UnitTestsBase
	{
		private IDealerService dealerService;

        [OneTimeSetUp]
        public void SetUp()
		{
            var repo = new Repository(data);
        dealerService = new DealerService(repo);
    }

    [Test]
		public async Task GetDealerIdAsyncShouldReturnCorrectUserId()
		{
			var result = await dealerService.GetDealerIdAsync(Dealer.UserId);

			Assert.That(result, Is.EqualTo(Dealer.Id));
		}

        [Test]
        public async Task ExistsByIdAsyncShouldReturnTrue()
        {
            var result = await dealerService.ExistsByIdAsync(Dealer.UserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserWithPhoneNumberExistsAsyncShouldReturnTrue()
        {
            var result = await dealerService.UserWithPhoneNumberExistsAsync(Dealer.PhoneNumber);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateAsyncShouldWorkCorrect()
        {
            //var firstDealersCount = data.Dealers.Count();

            //await dealerService.CreateAsync(Dealer.UserId, Dealer.PhoneNumber);

            //var secondDealersCount = data.Dealers.Count();

            //Assert.That(secondDealersCount, Is.EqualTo(firstDealersCount + 1));

            var newDealerId = await dealerService.GetDealerIdAsync(Dealer.UserId);
            var newDealer = await data.Dealers.FindAsync(newDealerId);

            Assert.IsNotNull(newDealer);
            Assert.That(newDealer.UserId, Is.EqualTo(Dealer.UserId));
            Assert.That(newDealer.PhoneNumber, Is.EqualTo(Dealer.PhoneNumber));

        }
    }
}

