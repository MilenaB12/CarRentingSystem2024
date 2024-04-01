using System;
using CarRentingSystem.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRentingSystem.Core.Constants.MessageConstants;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Car;
using CarRentingSystem.Core.Contracts;

namespace CarRentingSystem.Core.Models.Car
{
	public class CarFormModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = RequireMessage)]
        [StringLength(ColorMaxLength,
            MinimumLength = ColorMinLength,
            ErrorMessage = LengthMessage)]
        public string Color { get; set; } = string.Empty;

        [Range(YearMinValue, YearMaxValue)]
        public int? Year { get; set; }

        [Required(ErrorMessage = RequireMessage)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = LengthMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireMessage)]
        [EnumDataType(typeof(FuelType))]
        [Range(FuelMinValue, FuelMaxValue)]
        public FuelType FuelType { get; set; } 

        [Required(ErrorMessage = RequireMessage)]
        [EnumDataType(typeof(GearType))]
        public GearType GearType { get; set; }

        [Required(ErrorMessage = RequireMessage)]
        [Display(Name = "Price Per Month")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = RequireMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; }
            = new List<CarCategoryServiceModel>();

        public IEnumerable<CarBrandServiceModel> Brands { get; set; }
    = new List<CarBrandServiceModel>();
    }
}

