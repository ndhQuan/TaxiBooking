using TaxiBooking.Models;

namespace TaxiBooking.Repository.IRepository
{
    public interface IJourneyRepository : IRepository<JourneyLog>
    {
        Task<JourneyLog> UpdateAsync(JourneyLog entity);
    }
}
