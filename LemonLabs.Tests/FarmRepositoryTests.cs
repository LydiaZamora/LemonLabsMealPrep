using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LemonLabs.Tests
{
    public class FarmRepositoryTests
    {
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_PersistsNewFarm()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);
            var farm = new Farm { Name = "Test Farm", Location = "Denver, CO" };

            await repository.AddAsync(farm);

            Assert.Single(await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullWhenFarmDoesNotExist()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);

            var result = await repository.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_PersistsChanges()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);
            var farm = new Farm { Name = "Old Name", Location = "Denver, CO" };
            await repository.AddAsync(farm);

            farm.Name = "New Name";
            await repository.UpdateAsync(farm);

            var updated = await repository.GetByIdAsync(farm.Id);
            Assert.Equal("New Name", updated!.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesFarm()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);
            var farm = new Farm { Name = "Test Farm", Location = "Denver, CO" };
            await repository.AddAsync(farm);

            await repository.DeleteAsync(farm.Id);

            Assert.False(await repository.ExistsAsync(farm.Id));
        }

        [Fact]
        public async Task DeleteAsync_DoesNothingWhenFarmDoesNotExist()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);

            await repository.DeleteAsync(999);

            Assert.Empty(await repository.GetAllAsync());
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrueForPersistedFarm()
        {
            using var context = CreateContext();
            var repository = new FarmRepository(context);
            var farm = new Farm { Name = "Test Farm", Location = "Denver, CO" };
            await repository.AddAsync(farm);

            Assert.True(await repository.ExistsAsync(farm.Id));
        }
    }
}
