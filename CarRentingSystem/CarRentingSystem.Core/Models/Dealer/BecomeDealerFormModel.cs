using System;
using System.ComponentModel.DataAnnotations;
using static CarRentingSystem.Core.Constants.MessageConstants;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Dealer;
namespace CarRentingSystem.Core.Models.Dealer
{
	public class BecomeDealerFormModel
	{
		[Required(ErrorMessage = RequireMessage)]
		[StringLength(PhoneNumberMaxLength,
			MinimumLength = PhoneNumberMinLength,
			ErrorMessage = LengthMessage)]
		[Display(Name = "Phone number")]
		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}

