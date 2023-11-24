using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Repository
{
    public class JourneyRepository : Repository<JourneyLog>, IJourneyRepository
    {
        private readonly AppDbContext _db;
        public JourneyRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<JourneyLog> UpdateAsync(JourneyLog entity)
        {
            entity.LastUpdatedAt = DateTime.Now;
            _db.Journey.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
