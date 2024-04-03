using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.ApplicationUser;


namespace CarRentingSystem.Infrastructure.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		[MaxLength(FirstNameMaxLength)]
		[PersonalData]
		public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(LastNameMaxLength)]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

    }
}

