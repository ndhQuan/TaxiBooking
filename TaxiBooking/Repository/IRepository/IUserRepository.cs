using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using TaxiBooking.Models;

namespace TaxiBooking.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAllAsync(Expression<Func<AppUser, bool>>? filter = null);
        Task<AppUser> GetAsync(Expression<Func<AppUser, bool>> filter = null, bool tracked = true);
        Task<AppUser> UpdateAsync(AppUser entity);
    }
}
