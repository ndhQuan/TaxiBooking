using System.ComponentModel.DataAnnotations;

namespace TaxiBooking.Models.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
