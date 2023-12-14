using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Hubs
{
    public class JourneyHub:Hub
    {
        private readonly IUserRepository _dbUser;
        private readonly IDriverStateRepository _dbDriverState;
        public JourneyHub(IUserRepository dbUser, IDriverStateRepository dbDriverState)
        {
            _dbUser = dbUser;
            _dbDriverState = dbDriverState;

        }
        public async Task SendBookingRequest(string startAddr, float startLat, float startLng, string endAddr, float endLat, float endLng, int taxiTypeId)
        {
            Console.WriteLine("send");
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _dbUser.GetAsync(u=>u.Id == userId);
            var name = user.Name;
            var phoneNumber = user.PhoneNumber;

            var driver = await _dbDriverState.GetAsync(u => u.TrangThai == 1);

            await Clients.User(driver.DriverId).SendAsync("ReceiveRequestInfo", userId, name, phoneNumber, startAddr, startLat, startLng, endAddr, endLat, endLng);
        }
        public async Task SendDenyResponse(string userId)
        {
            var DriverId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId + "  " + DriverId);
            await Clients.User(userId).SendAsync("ReceiveDenyResponse", DriverId);
        }
        public async Task SendDriverInfo(string customerId)
        {
            var DriverId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var driver = await _dbUser.GetAsync(u => u.Id == DriverId);
            var driverState = await _dbDriverState.GetAsync(u => u.DriverId == DriverId);
            var name = driver.Name;
            var phoneNumber = driver.PhoneNumber;
            var licensePlate = driverState.BienSoXe;
            Console.WriteLine(customerId + "customer");
            await Clients.User(customerId).SendAsync("ReceivedDriverInfo", name, phoneNumber, licensePlate);
        }
        public async Task SendRealTimeDriverCoords(string guestId, float lat, float lng)
        {
            var DriverId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var driver = await _dbUser.GetAsync(u => u.Id == DriverId);
            var driverState = await _dbDriverState.GetAsync(u=>u.DriverId == DriverId);
            Console.WriteLine(guestId + "guest");
            await Clients.User(guestId).SendAsync("ReceivedDriverCoords", lat, lng);
        }
        public async Task SendPickedupNotification(string guestId)
        {
            Console.WriteLine("Notification");
            await Clients.User(guestId).SendAsync("ReceivedPickedupNotification");
        }
    }
}
