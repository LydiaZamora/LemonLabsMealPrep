using LemonLabs.Web.Models;

namespace LemonLabs.Web.Repositories
{
    public interface IFarmRepository
    {
        Task<List<Farm>> GetAllAsync();
        Task<Farm?> GetByIdAsync(int id);
        Task AddAsync(Farm farm);
        Task UpdateAsync(Farm farm);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
