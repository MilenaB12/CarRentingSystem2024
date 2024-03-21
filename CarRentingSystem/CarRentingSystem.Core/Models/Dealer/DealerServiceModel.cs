using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.Core.Models.Dealer
{
	public class DealerServiceModel
	{
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}

