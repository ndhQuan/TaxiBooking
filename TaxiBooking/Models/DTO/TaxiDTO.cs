using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBooking.Models.DTO
{
    public class TaxiDTO
    {
        [Required]
        public string TaxiId { get; set; }
        public int? TypeId { get; set; }
        public TaxiType Type { get; set; }
    }
}
