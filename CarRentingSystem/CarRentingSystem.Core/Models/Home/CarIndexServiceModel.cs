using System;
using CarRentingSystem.Infrastructure.Data.Models;

namespace CarRentingSystem.Core.Models.Home
{
	public class CarIndexServiceModel
	{
		public int Id { get; set; }

		public string Brand { get; set; } = null!;

        public string ImageUrl { get; set; } = string.Empty;


    }
}

