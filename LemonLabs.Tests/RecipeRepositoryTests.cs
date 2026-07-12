using LemonLabs.Web.Data;
using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LemonLabs.Tests
{
    public class RecipeRepositoryTests
    {
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_PersistsNewRecipe()
        {
            using var context = CreateContext();
            var repository = new RecipeRepository(context);
            var recipe = new Recipe { Name = "Test Recipe", DietType = DietType.Vegan, Category = MealCategory.Lunch, Calories = 400, PricePerServing = 6m };

            await repository.AddAsync(recipe);

            Assert.Single(await repository.GetAllAsync());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsRecipesInInsertOrder()
        {
            using var context = CreateContext();
            var repository = new RecipeRepository(context);
            await repository.AddAsync(new Recipe { Name = "First", DietType = DietType.Vegan, Category = MealCategory.Breakfast, Calories = 300, PricePerServing = 4m });
            await repository.AddAsync(new Recipe { Name = "Second", DietType = DietType.Keto, Category = MealCategory.Dinner, Calories = 500, PricePerServing = 9m });

            var recipes = await repository.GetAllAsync();

            Assert.Equal(2, recipes.Count);
        }

        [Fact]
        public async Task UpdateAsync_PersistsChanges()
        {
            using var context = CreateContext();
            var repository = new RecipeRepository(context);
            var recipe = new Recipe { Name = "Original", DietType = DietType.Vegan, Category = MealCategory.Lunch, Calories = 400, PricePerServing = 6m };
            await repository.AddAsync(recipe);

            recipe.Calories = 550;
            await repository.UpdateAsync(recipe);

            var updated = await repository.GetByIdAsync(recipe.Id);
            Assert.Equal(550, updated!.Calories);
        }

        [Fact]
        public async Task DeleteAsync_RemovesRecipe()
        {
            using var context = CreateContext();
            var repository = new RecipeRepository(context);
            var recipe = new Recipe { Name = "Test Recipe", DietType = DietType.Vegan, Category = MealCategory.Lunch, Calories = 400, PricePerServing = 6m };
            await repository.AddAsync(recipe);

            await repository.DeleteAsync(recipe.Id);

            Assert.False(await repository.ExistsAsync(recipe.Id));
        }
    }
}
