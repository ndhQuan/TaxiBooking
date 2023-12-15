using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Hubs
{
    public class JourneyHub:Hub
    {
        private readonly IUserRepository _dbUser;
        private readonly IDriverStateRepository _dbDriverState;
        private readonly ITaxiRepository _dbTaxi;
        public static Dictionary<string, List<string>> denidedList = new Dictionary<string, List<string>>();

        public JourneyHub(IUserRepository dbUser, IDriverStateRepository dbDriverState, ITaxiRepository dbTaxi)
        {
            _dbUser = dbUser;
            _dbDriverState = dbDriverState;
            _dbTaxi = dbTaxi;
        }
        public async Task SendBookingRequest(string startAddr, float startLat, float startLng, string endAddr, float endLat, float endLng, int taxiTypeId)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(userId);
            Console.WriteLine("send");
            var user = await _dbUser.GetAsync(u=>u.Id == userId);
            var name = user.Name;
            var phoneNumber = user.PhoneNumber;

            //var driverAvailable = await _dbDriverState.GetAllAsync(u => u.TrangThai == 1);
            var taxi = await _dbTaxi.GetAllAsync(u => u.TypeId == taxiTypeId);
            var taxiId = taxi.Select(u => u.TaxiId);
            var driver = await _dbDriverState.GetAllAsync(u => (u.TrangThai == 1 && taxiId.Contains(u.BienSoXe)));
            //var driver = await _dbDriverState.GetAsync(u => (u.TrangThai == 1));
            var driverId = driver.Select(u => u.DriverId).ToList();

            if(denidedList.ContainsKey(userId) && denidedList[userId].Count > 0)
            {
                foreach(string deniedDriver in denidedList[userId])
                {
                    driverId.Remove(deniedDriver);
                }
            }
            if(driverId.Count == 0)
            {
                await Clients.Caller.SendAsync("NotFoundDriver");
                return;
            }

            Console.WriteLine(driverId.FirstOrDefault());
            await Clients.User(driverId.FirstOrDefault()).SendAsync("ReceiveRequestInfo", userId, name, phoneNumber, startAddr, startLat, startLng, endAddr, endLat, endLng);
        }
        public async Task SendDenyResponse(string userId)
        {
            var DriverId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (denidedList.ContainsKey(userId))
            {
                denidedList[userId].Add(DriverId);
            }
            else
            {
                denidedList.Add(userId, new List<string> { DriverId });
            }
            Console.WriteLine(userId + "  " + DriverId);
            await Clients.User(userId).SendAsync("ReceiveDenyResponse");
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
            if (denidedList.ContainsKey(customerId))
            {
                denidedList.Remove(customerId);
            }

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
        public void RefreshDeniedList()
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (denidedList.ContainsKey(userId))
            {
                denidedList.Remove(userId);
            }
        }
    }
}
