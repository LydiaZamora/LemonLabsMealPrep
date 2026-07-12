using LemonLabs.Web.Models;
using LemonLabs.Web.Services;
using Xunit;

namespace LemonLabs.Tests
{
    public class MealPlanServiceTests
    {
        private readonly MealPlanService _service = new();

        private static List<Recipe> SampleRecipes() => new()
        {
            new Recipe { Id = 1, Name = "Vegan Bowl", DietType = DietType.Vegan, Category = MealCategory.Breakfast, Calories = 400, PricePerServing = 5m, Allergens = "None" },
            new Recipe { Id = 2, Name = "Vegan Salad", DietType = DietType.Vegan, Category = MealCategory.Lunch, Calories = 450, PricePerServing = 6m, Allergens = "Peanuts" },
            new Recipe { Id = 3, Name = "Chicken Dinner", DietType = DietType.Paleo, Category = MealCategory.Dinner, Calories = 600, PricePerServing = 12m, Allergens = "None" },
            new Recipe { Id = 4, Name = "Omnivore Snack", DietType = DietType.Omnivore, Category = MealCategory.Snack, Calories = 200, PricePerServing = 3m, Allergens = "Dairy" },
        };

        [Fact]
        public void GenerateDailyPlan_ExcludesRecipesIncompatibleWithDiet()
        {
            var profile = new UserProfile { DietType = DietType.Vegan, DailyCalorieGoal = 3000, WeeklyBudget = 700 };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());

            Assert.All(plan.Meals, m => Assert.Equal(DietType.Vegan, m.DietType));
        }

        [Fact]
        public void GenerateDailyPlan_ExcludesRecipesContainingUserAllergens()
        {
            var profile = new UserProfile { DietType = DietType.Vegan, Allergies = "Peanuts", DailyCalorieGoal = 3000, WeeklyBudget = 700 };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());

            Assert.DoesNotContain(plan.Meals, m => m.Name == "Vegan Salad");
        }

        [Fact]
        public void GenerateDailyPlan_OmnivoreProfileCanReceiveAnyDietRecipes()
        {
            var profile = new UserProfile { DietType = DietType.Omnivore, DailyCalorieGoal = 3000, WeeklyBudget = 700 };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());

            Assert.Contains(plan.Meals, m => m.DietType == DietType.Paleo);
        }

        [Fact]
        public void GenerateDailyPlan_DoesNotExceedDailyCalorieGoal()
        {
            var profile = new UserProfile { DietType = DietType.Omnivore, DailyCalorieGoal = 500, WeeklyBudget = 700 };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());

            Assert.True(plan.TotalCalories <= 500);
        }

        [Fact]
        public void GenerateDailyPlan_DoesNotExceedDailyBudget()
        {
            var profile = new UserProfile { DietType = DietType.Omnivore, DailyCalorieGoal = 3000, WeeklyBudget = 7m };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());
            var dailyBudget = profile.WeeklyBudget / 7m;

            Assert.True(plan.TotalCost <= dailyBudget);
        }

        [Fact]
        public void GenerateDailyPlan_ReturnsEmptyPlanWhenNoCandidatesMatchDiet()
        {
            var profile = new UserProfile { DietType = DietType.Vegan, DailyCalorieGoal = 3000, WeeklyBudget = 700 };
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "Steak", DietType = DietType.Paleo, Category = MealCategory.Dinner, Calories = 600, PricePerServing = 10m }
            };

            var plan = _service.GenerateDailyPlan(profile, recipes);

            Assert.Empty(plan.Meals);
            Assert.Equal(0, plan.TotalCalories);
            Assert.Equal(0m, plan.TotalCost);
        }

        [Fact]
        public void GenerateDailyPlan_ThrowsWhenProfileIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _service.GenerateDailyPlan(null!, SampleRecipes()));
        }

        [Fact]
        public void GenerateDailyPlan_VegetarianProfileAcceptsVeganRecipes()
        {
            var profile = new UserProfile { DietType = DietType.Vegetarian, DailyCalorieGoal = 3000, WeeklyBudget = 700 };

            var plan = _service.GenerateDailyPlan(profile, SampleRecipes());

            Assert.Contains(plan.Meals, m => m.DietType == DietType.Vegan);
            Assert.DoesNotContain(plan.Meals, m => m.DietType == DietType.Paleo);
        }
    }
}
