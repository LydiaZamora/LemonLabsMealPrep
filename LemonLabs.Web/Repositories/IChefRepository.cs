using LemonLabs.Web.Models;

namespace LemonLabs.Web.Repositories
{
    public interface IChefRepository
    {
        Task<List<Chef>> GetAllAsync();
        Task<Chef?> GetByIdAsync(int id);
        Task AddAsync(Chef chef);
        Task UpdateAsync(Chef chef);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
