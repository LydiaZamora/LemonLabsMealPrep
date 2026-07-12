using LemonLabs.Web.Models;

namespace LemonLabs.Web.Repositories
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAllAsync();
        Task<UserProfile?> GetByIdAsync(int id);
        Task AddAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
