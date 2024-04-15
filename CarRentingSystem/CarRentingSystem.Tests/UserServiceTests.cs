using System;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data.Common;

namespace CarRentingSystem.Tests
{
	public class UserServiceTests : UnitTestsBase
	{
		private IUserService userService;
        [OneTimeSetUp]
        public void SetUp()
		{
            var repo = new Repository(data);
            userService = new UserService(repo);
        }

        [Test]
        public async Task UserHasRentsAsyncShouldReturnCorrectData()
        {
            var result = await userService.UserFullName(Renter.Id);

            var renterFullName = $"{Renter.FirstName} {Renter.LastName}";

            Assert.That(result, Is.EqualTo(renterFullName));
        }

        [Test]
        public async Task AllAsyncShouldReturnCorrectData()
        {
            var result = await userService.AllAsync();

            var usersCount = data.Users.Count();
            var expectedUsers = result.ToList();

            Assert.That(expectedUsers.Count(), Is.EqualTo(usersCount));
        }
    }
}

