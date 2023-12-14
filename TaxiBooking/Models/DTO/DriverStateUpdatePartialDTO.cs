using System.ComponentModel.DataAnnotations;

namespace TaxiBooking.Models.DTO
{
    public class DriverStateUpdatePartialDTO
    {
        public string DriverId { get; set; }
        public float latCurrent { get; set; }
        public float longCurrent { get; set; }
        public string TinhTP { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public string Duong { get; set; }
        [Range(0, 2)]
        public int TrangThai { get; set; }
        public string BienSoXe { get; set; }

    }
}
