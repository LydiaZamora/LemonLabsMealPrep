using LemonLabs.Web.Models;

namespace LemonLabs.Web.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task AddAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
