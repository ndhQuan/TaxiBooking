using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Repository
{
    public class DriverStateRepository : Repository<DriverState>, IDriverStateRepository
    {
        private readonly AppDbContext _db;
        public DriverStateRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<DriverState> UpdateAsync(DriverState entity)
        {
            entity.LastUpdatedAt = DateTime.Now;
            _db.DriverState.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
