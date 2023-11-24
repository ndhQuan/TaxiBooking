using TaxiBooking.Models;

namespace TaxiBooking.Repository.IRepository
{
    public interface ITaxiTypeRepository : IRepository<TaxiType>
    {
        Task<TaxiType> UpdateAsync(TaxiType entity);
    }
}
