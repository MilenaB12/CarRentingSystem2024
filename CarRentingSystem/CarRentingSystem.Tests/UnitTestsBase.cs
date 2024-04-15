using System;
using AutoMapper;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using CarRentingSystem.Tests.Mocks;

namespace CarRentingSystem.Tests
{
	public class UnitTestsBase
	{
		protected CarRentingDbContext data;

		protected IMapper mapper;

		[OneTimeSetUp]
		public void SetUpBase()
		{
            data = DatabaseMock.Instance;
            SeedDatabase();
		}

		public ApplicationUser Renter { get; private set; }

        public Dealer Dealer { get; private set; }

        public Car Car { get; private set; }

		private void SeedDatabase()
		{
			Renter = new ApplicationUser()
			{
				Id = "RenterId",
				Email = "renter@abv",
				FirstName = "Tom",
				LastName = "Tomov"
			};
			data.Users.Add(Renter);

			Dealer = new Dealer()
			{
				PhoneNumber = "+359333333333",
				User = new ApplicationUser()
				{
					Id = "UserId",
					Email = "someuser@abv",
					FirstName = "Sam",
					LastName = "Popov"
				}
			};
			data.Dealers.Add(Dealer);

			Car = new Car()
			{
				Color = "pink",
				Price = 3400,
				Brand = new Brand { Name = "Audi" },
				Category = new Category { Name = "Sedan" },
				Description = "A lot of description!",
				Location = new Location { Name = "Varna" },
				Dealer = Dealer,
				Renter = Renter,
				FuelType = Infrastructure.Enums.FuelType.Diesel,
				GearType = Infrastructure.Enums.GearType.Automatic,
				ImageUrl = "",
				Year = 2010,
				IsApproved = true
			};
			data.Cars.Add(Car);
			data.SaveChanges();
		}

		[OneTimeTearDown]
		public void TearDown()
			=> data.Dispose();
    }
}

