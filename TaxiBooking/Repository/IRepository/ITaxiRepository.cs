using TaxiBooking.Models;

namespace TaxiBooking.Repository.IRepository
{
    public interface ITaxiRepository : IRepository<Taxi>
    {
        Task<Taxi> UpdateAsync(Taxi entity);
    }
}
