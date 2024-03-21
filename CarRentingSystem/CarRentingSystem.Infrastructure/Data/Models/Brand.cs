using System.ComponentModel.DataAnnotations;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Brand;

namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IList<Car> Cars { get; set; } = new List<Car>();
    }
}