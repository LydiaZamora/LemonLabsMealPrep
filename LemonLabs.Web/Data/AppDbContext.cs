using LemonLabs.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonLabs.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Chef> Chefs { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>().HasData(
                new Farm { Id = 1, Name = "Green Valley Farm", Location = "Boulder, CO", Description = "Family-owned organic vegetable farm." },
                new Farm { Id = 2, Name = "Sunrise Pastures", Location = "Fort Collins, CO", Description = "Pasture-raised poultry and eggs." },
                new Farm { Id = 3, Name = "Rocky Creek Orchards", Location = "Longmont, CO", Description = "Seasonal fruit orchard." }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Kale", IsOrganic = true, SeasonalAvailability = "Fall-Winter", FarmId = 1 },
                new Ingredient { Id = 2, Name = "Sweet Potato", IsOrganic = true, SeasonalAvailability = "Fall", FarmId = 1 },
                new Ingredient { Id = 3, Name = "Free-Range Eggs", IsOrganic = true, SeasonalAvailability = "Year-round", FarmId = 2 },
                new Ingredient { Id = 4, Name = "Chicken Breast", IsOrganic = false, SeasonalAvailability = "Year-round", FarmId = 2 },
                new Ingredient { Id = 5, Name = "Blueberries", IsOrganic = true, SeasonalAvailability = "Summer", FarmId = 3 }
            );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Name = "Kale & Sweet Potato Bowl", Description = "Roasted sweet potato over sauteed kale.", DietType = DietType.Vegan, Category = MealCategory.Lunch, Calories = 480, PricePerServing = 8.50m, Allergens = "None" },
                new Recipe { Id = 2, Name = "Grilled Chicken & Eggs Breakfast", Description = "High-protein breakfast plate.", DietType = DietType.Paleo, Category = MealCategory.Breakfast, Calories = 520, PricePerServing = 7.00m, Allergens = "Eggs" },
                new Recipe { Id = 3, Name = "Blueberry Overnight Oats", Description = "Vegetarian oats with fresh blueberries.", DietType = DietType.Vegetarian, Category = MealCategory.Breakfast, Calories = 350, PricePerServing = 5.25m, Allergens = "Gluten" }
            );

            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient { Id = 1, RecipeId = 1, IngredientId = 1, Quantity = "2 cups" },
                new RecipeIngredient { Id = 2, RecipeId = 1, IngredientId = 2, Quantity = "1 medium" },
                new RecipeIngredient { Id = 3, RecipeId = 2, IngredientId = 3, Quantity = "2 eggs" },
                new RecipeIngredient { Id = 4, RecipeId = 2, IngredientId = 4, Quantity = "4 oz" },
                new RecipeIngredient { Id = 5, RecipeId = 3, IngredientId = 5, Quantity = "1/2 cup" }
            );

            modelBuilder.Entity<Chef>().HasData(
                new Chef { Id = 1, Name = "Chef Maria Alvarez", Bio = "Plant-forward seasonal cooking.", Specialty = DietType.Vegan, YearsExperience = 8, HourlyRate = 65m },
                new Chef { Id = 2, Name = "Chef Daniel Osei", Bio = "High-protein performance meals.", Specialty = DietType.Paleo, YearsExperience = 12, HourlyRate = 85m }
            );
        }
    }
}
