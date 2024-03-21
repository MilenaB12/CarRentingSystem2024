using System;
using CarRentingSystem.Core.Models.Dealer;

namespace CarRentingSystem.Core.Models.Car
{
	public class CarDetailsServiceModel : CarServiceModel
	{
		public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string FuelType { get; set; } = null!;

        public string GearType { get; set; } = null!;

        public int? Year { get; set; } 

        public DealerServiceModel Dealer { get; set; } = null!;


    }
}

