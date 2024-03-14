using System;
using Microsoft.AspNetCore.Identity;

namespace CarRentingSystem.Infrastructure.Data.Models
{
	public class ApplicationUser : IdentityUser<int>
	{
		public IList<Car> RentedCars { get; set; } = new List<Car>();
	}
}

