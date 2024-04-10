using System;
using CarRentingSystem.Core.Contracts;

namespace CarRentingSystem.Core.Models.Car
{
	public class CarDetailsViewModel
    {
		public int Id { get; set; }

		public string Brand { get; set; } = string.Empty;

		public string Location { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

    }
}

