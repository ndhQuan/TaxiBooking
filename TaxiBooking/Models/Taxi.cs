using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBooking.Models
{
    public class Taxi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TaxiId { get; set; }
        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public TaxiType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}

    }
}
