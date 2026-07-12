using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonLabs.Web.Repositories
{
    public class FarmRepository : IFarmRepository
    {
        private readonly AppDbContext _context;

        public FarmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Farm>> GetAllAsync() =>
            await _context.Farms.Include(f => f.Ingredients).AsNoTracking().ToListAsync();

        public async Task<Farm?> GetByIdAsync(int id) =>
            await _context.Farms.Include(f => f.Ingredients).FirstOrDefaultAsync(f => f.Id == id);

        public async Task AddAsync(Farm farm)
        {
            _context.Farms.Add(farm);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Farm farm)
        {
            _context.Farms.Update(farm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var farm = await _context.Farms.FindAsync(id);
            if (farm != null)
            {
                _context.Farms.Remove(farm);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.Farms.AnyAsync(f => f.Id == id);
    }
}
