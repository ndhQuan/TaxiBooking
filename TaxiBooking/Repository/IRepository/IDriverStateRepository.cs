using TaxiBooking.Models;

namespace TaxiBooking.Repository.IRepository
{
    public interface IDriverStateRepository : IRepository<DriverState>
    {
        Task<DriverState> UpdateAsync(DriverState entity);
    }
}
