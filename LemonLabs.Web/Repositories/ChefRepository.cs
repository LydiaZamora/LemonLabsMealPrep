using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonLabs.Web.Repositories
{
    public class ChefRepository : IChefRepository
    {
        private readonly AppDbContext _context;

        public ChefRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Chef>> GetAllAsync() =>
            await _context.Chefs.AsNoTracking().ToListAsync();

        public async Task<Chef?> GetByIdAsync(int id) =>
            await _context.Chefs.FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Chef chef)
        {
            _context.Chefs.Add(chef);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chef chef)
        {
            _context.Chefs.Update(chef);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            if (chef != null)
            {
                _context.Chefs.Remove(chef);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.Chefs.AnyAsync(c => c.Id == id);
    }
}
