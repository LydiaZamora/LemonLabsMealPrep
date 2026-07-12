using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonLabs.Web.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ingredient>> GetAllAsync() =>
            await _context.Ingredients.Include(i => i.Farm).AsNoTracking().ToListAsync();

        public async Task<Ingredient?> GetByIdAsync(int id) =>
            await _context.Ingredients.Include(i => i.Farm).FirstOrDefaultAsync(i => i.Id == id);

        public async Task AddAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id) =>
            await _context.Ingredients.AnyAsync(i => i.Id == id);
    }
}
