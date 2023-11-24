using System.ComponentModel.DataAnnotations;

namespace TaxiBooking.Models.DTO
{
    public class DriverStateCreateDTO
    {
        public string DriverId { get; set; }
        public AppUser Driver { get; set; }
        public float? latCurrent { get; set; }
        public float? longCurrent { get; set; }
        public string TinhTP { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public string Duong { get; set; }
        [MaxLength(50)]
        public int TrangThai { get; set; }
        public string BienSoXe { get; set; }
    }
}
