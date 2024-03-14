using System.ComponentModel.DataAnnotations;
using static CarRentingSystem.Infrastructure.Constants.EntityValidationConstants.Dealer;


namespace CarRentingSystem.Infrastructure.Data.Models
{
    public class Dealer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public IList<Car> Cars { get; set; } = new List<Car>();

        public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}