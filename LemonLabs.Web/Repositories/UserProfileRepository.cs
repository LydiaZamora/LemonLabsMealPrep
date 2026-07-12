using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonLabs.Web.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;

        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfile>> GetAllAsync() =>
            await _context.UserProfiles.AsNoTracking().ToListAsync();

        public async Task<UserProfile?> GetByIdAsync(int id) =>
            await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == id);

        public async Task AddAsync(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.UserProfiles.AnyAsync(u => u.Id == id);
    }
}
