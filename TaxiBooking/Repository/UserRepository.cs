using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TaxiBooking.DataContext;
using TaxiBooking.Models;
using TaxiBooking.Repository.IRepository;

namespace TaxiBooking.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        internal DbSet<AppUser> dbSet;
        public UserRepository(AppDbContext db)
        {
            _db = db;
            dbSet = _db.Set<AppUser>();
        }
        public async Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>> filter = null)
        {
            IQueryable<AppUser> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> filter = null, bool tracked = false)
        {
            IQueryable<AppUser> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<AppUser> UpdateAsync(AppUser entity)
        {
            entity.LastUpdatedAt = DateTime.Now;
            _db.AppUsers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
