using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRentingSystem.Infrastructure.Enums;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Car;



namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = string.Empty;

        public int Year { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public GearType GearType { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int DealerId { get; set; }

        [ForeignKey(nameof(DealerId))]
        public Dealer Dealer { get; set; } = null!;

        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Brand Brand { get; set; } = null!;

        public string? RenterId { get; set; }

    }
}