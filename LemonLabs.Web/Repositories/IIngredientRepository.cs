using LemonLabs.Web.Models;

namespace LemonLabs.Web.Repositories
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> GetAllAsync();
        Task<Ingredient?> GetByIdAsync(int id);
        Task AddAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
