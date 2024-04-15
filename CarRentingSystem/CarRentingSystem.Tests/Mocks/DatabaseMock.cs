using System;
using CarRentingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentingSystem.Tests.Mocks
{
	public static class DatabaseMock
	{
		public static CarRentingDbContext Instance
		{
			get
			{
				var dbContextOptions = new DbContextOptionsBuilder<CarRentingDbContext>()
					.UseInMemoryDatabase("CarRentingInMemoryDb"
					+ DateTime.Now.Ticks.ToString())
					.Options;

				return new CarRentingDbContext(dbContextOptions, false);
			}
		}
	}
}

