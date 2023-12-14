using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiBooking.Models
{
    public class DriverState
    {
        [Key]
        [ForeignKey("Driver")]
        public string DriverId { get; set; }
        public AppUser Driver { get; set; }
        public float? latCurrent { get; set; }
        public float? longCurrent { get; set; }
        public string TinhTP { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public string Duong { get; set; }
        [Range(0, 2)]
        public int TrangThai { get; set; }
        [ForeignKey("Taxi")]
        public string BienSoXe { get; set; }
        public Taxi Taxi { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
