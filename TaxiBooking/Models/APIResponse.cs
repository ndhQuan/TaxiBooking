using System.Net;

namespace TaxiBooking.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            this.ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
