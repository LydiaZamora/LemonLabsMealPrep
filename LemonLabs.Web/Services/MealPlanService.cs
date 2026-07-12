using LemonLabs.Web.Models;

namespace LemonLabs.Web.Services
{
    public class MealPlanService : IMealPlanService
    {
        // Diets a recipe of a given DietType is safe to serve to a user following each DietType.
        // e.g. a Vegan recipe satisfies Vegan, Vegetarian, Pescatarian and Omnivore eaters.
        private static readonly Dictionary<DietType, HashSet<DietType>> CompatibleRecipeDietsFor = new()
        {
            [DietType.Vegan] = new HashSet<DietType> { DietType.Vegan },
            [DietType.Vegetarian] = new HashSet<DietType> { DietType.Vegan, DietType.Vegetarian },
            [DietType.Pescatarian] = new HashSet<DietType> { DietType.Vegan, DietType.Vegetarian, DietType.Pescatarian },
            [DietType.GlutenFree] = new HashSet<DietType> { DietType.GlutenFree },
            [DietType.Keto] = new HashSet<DietType> { DietType.Keto },
            [DietType.Paleo] = new HashSet<DietType> { DietType.Paleo },
            [DietType.Omnivore] = Enum.GetValues<DietType>().ToHashSet(),
        };

        public MealPlanResult GenerateDailyPlan(UserProfile profile, IEnumerable<Recipe> candidateRecipes)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var allergies = (profile.Allergies ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(a => a.ToLowerInvariant())
                .ToList();

            var compatibleDiets = CompatibleRecipeDietsFor.TryGetValue(profile.DietType, out var diets)
                ? diets
                : new HashSet<DietType> { profile.DietType };

            var eligible = candidateRecipes
                .Where(r => compatibleDiets.Contains(r.DietType))
                .Where(r => !ContainsAllergen(r.Allergens, allergies))
                .ToList();

            var dailyBudget = profile.WeeklyBudget / 7m;

            var result = new MealPlanResult();

            foreach (var category in new[] { MealCategory.Breakfast, MealCategory.Lunch, MealCategory.Dinner })
            {
                var pick = eligible
                    .Where(r => r.Category == category)
                    .Where(r => result.TotalCost + r.PricePerServing <= dailyBudget || dailyBudget <= 0)
                    .Where(r => result.TotalCalories + r.Calories <= profile.DailyCalorieGoal)
                    .OrderBy(r => r.PricePerServing)
                    .FirstOrDefault();

                if (pick != null)
                {
                    result.Meals.Add(pick);
                    result.TotalCalories += pick.Calories;
                    result.TotalCost += pick.PricePerServing;
                }
            }

            return result;
        }

        private static bool ContainsAllergen(string? recipeAllergens, List<string> userAllergies)
        {
            if (userAllergies.Count == 0 || string.IsNullOrWhiteSpace(recipeAllergens))
                return false;

            var recipeAllergenList = recipeAllergens
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(a => a.ToLowerInvariant());

            return recipeAllergenList.Any(userAllergies.Contains);
        }
    }
}
