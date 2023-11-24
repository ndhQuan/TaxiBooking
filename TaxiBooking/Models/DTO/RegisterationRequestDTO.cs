namespace TaxiBooking.Models.Dto
{
    public class RegisterationRequestDTO
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
