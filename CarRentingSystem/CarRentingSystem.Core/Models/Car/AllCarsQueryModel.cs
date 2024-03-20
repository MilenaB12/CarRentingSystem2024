 using System;
using System.ComponentModel.DataAnnotations;
using CarRentingSystem.Core.Enumerations;

namespace CarRentingSystem.Core.Models.Car
{
	public class AllCarsQueryModel
	{
		public int CarsPerPage { get; } = 3;

		public string Category { get; set; } = null!;

		[Display(Name = "Search by text")]
		public string SearchTerm { get; set; } = null!;

        public CarSorting Sorting { get; set; }

		public int CurrentPage { get; set; } = 1;

        public int TotalCarsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

		public IEnumerable<CarServiceModel> Cars { get; set; }
		= new List<CarServiceModel>();
	}
}

