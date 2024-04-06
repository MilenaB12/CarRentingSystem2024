using System;
namespace CarRentingSystem.Core.Models.Admin
{
	public class RentServiceModel
	{
        public string CarBrand { get; set; } = string.Empty;

        public string CarImageUrl { get; set; } = string.Empty;

        public string DealerFullName { get; set; } = string.Empty;

        public string DealerEmail { get; set; } = string.Empty;

        public string RenterFullName { get; set; } = string.Empty;

        public string RenterEmail { get; set; } = string.Empty;

    }
}

