using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Repository
{
    public class TaxiTypeRepository : Repository<TaxiType>, ITaxiTypeRepository
    {
        private readonly AppDbContext _db;
        public TaxiTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<TaxiType> UpdateAsync(TaxiType entity)
        {
            _db.TaxiTypes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
