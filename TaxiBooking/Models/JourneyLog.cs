using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBooking.Models
{
    public class JourneyLog
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public AppUser Customer { get; set; }
        [ForeignKey("Driver")]
        public string DriverId { get; set; }
        public AppUser Driver { get; set; }
        [ForeignKey("Taxi")]
        public string LicensePlate { get; set; }
        public Taxi Taxi { get; set; }
        public string StartAddr { get; set; }
        public float StartLat { get; set; }
        public float StartLng { get; set; }
        public string EndAddr { get; set; }
        public float EndLat { get; set; }
        public float EndLng { get; set; }
        public float Distance { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public int State { get; set; }
        [Range(1,5)]
        public int? Rating { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
