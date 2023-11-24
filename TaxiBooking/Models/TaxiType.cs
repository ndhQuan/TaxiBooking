using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBooking.Models
{
    public class TaxiType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int NumberOfSeat { get; set; }
        public float Cost { get; set; }
    }
}
