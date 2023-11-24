using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiBooking.Models.DTO
{
    public class TaxiCreateDTO
    {
        [Required]
        public string TaxiId { get; set; }
        public int? TypeId { get; set; }
    }
}
