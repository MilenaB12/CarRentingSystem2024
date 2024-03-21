using System;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Infrastructure.Data.Models;

namespace System.Linq
{
	public static class IQuerableCarExtension
	{
		public static IQueryable<CarServiceModel> ProjectToCarServiceModel(this IQueryable<Car> cars)
		{
			return cars
				.Select(c => new CarServiceModel()
				{
					Id = c.Id,
					Color = c.Color,
					Brand = c.Brand.Name,
					ImageUrl = c.ImageUrl,
					IsRented = c.RenterId != null,
					Price = c.Price
			    });
		}
	}
}

