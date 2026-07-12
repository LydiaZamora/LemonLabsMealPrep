using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LemonLabs.Tests
{
    public class UserProfileRepositoryTests
    {
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_PersistsNewProfile()
        {
            using var context = CreateContext();
            var repository = new UserProfileRepository(context);
            var profile = new UserProfile { Name = "Jamie Rivera", Email = "jamie@example.com", DietType = DietType.Vegetarian, DailyCalorieGoal = 2200, WeeklyBudget = 120m };

            await repository.AddAsync(profile);

            Assert.Single(await repository.GetAllAsync());
        }

        [Fact]
        public async Task UpdateAsync_PersistsDietaryChanges()
        {
            using var context = CreateContext();
            var repository = new UserProfileRepository(context);
            var profile = new UserProfile { Name = "Jamie Rivera", Email = "jamie@example.com", DietType = DietType.Omnivore, DailyCalorieGoal = 2200, WeeklyBudget = 120m };
            await repository.AddAsync(profile);

            profile.DietType = DietType.Vegan;
            profile.Allergies = "Shellfish";
            await repository.UpdateAsync(profile);

            var updated = await repository.GetByIdAsync(profile.Id);
            Assert.Equal(DietType.Vegan, updated!.DietType);
            Assert.Equal("Shellfish", updated.Allergies);
        }

        [Fact]
        public async Task DeleteAsync_RemovesProfile()
        {
            using var context = CreateContext();
            var repository = new UserProfileRepository(context);
            var profile = new UserProfile { Name = "Jamie Rivera", Email = "jamie@example.com", DietType = DietType.Omnivore, DailyCalorieGoal = 2200, WeeklyBudget = 120m };
            await repository.AddAsync(profile);

            await repository.DeleteAsync(profile.Id);

            Assert.False(await repository.ExistsAsync(profile.Id));
        }
    }
}
