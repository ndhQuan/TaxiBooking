using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Repository
{
    public class TaxiRepository : Repository<Taxi>, ITaxiRepository
    {
        private readonly AppDbContext _db;
        public TaxiRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Taxi> UpdateAsync(Taxi entity)
        {
            entity.LastUpdatedAt = DateTime.Now;
            _db.Taxis.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
