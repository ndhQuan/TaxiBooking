namespace TaxiBooking.Models.DTO
{
    public class JourneyCreateDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string DriverId { get; set; }
        public string LicensePlate { get; set; }
        public string StartPos { get; set; }
        public float LatStart { get; set; }
        public float LongStart { get; set; }
        public string DesPos { get; set; }
        public float LatDes { get; set; }
        public float LongDes { get; set; }
        public float Distance { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public int State { get; set; }
        public int Rating { get; set; }
        public float TotalPrice { get; set; }
    }
}
