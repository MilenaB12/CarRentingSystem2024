using System.ComponentModel.DataAnnotations;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Infrastructure.Data.Models;
using static CarRentingSystem.Core.Constants.MessageConstants;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Car;

namespace CarRentingSystem.Core.Models.Car
{
    public class CarServiceModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = RequireMessage)]
        [StringLength(ColorMaxLength,
    MinimumLength = ColorMinLength,
    ErrorMessage = LengthMessage)]
        public string Color { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireMessage)]
        [Display(Name = "Price per month")]
        public decimal Price { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}