using LemonLabs.Web.Models;

namespace LemonLabs.Web.Services
{
    public interface IMealPlanService
    {
        MealPlanResult GenerateDailyPlan(UserProfile profile, IEnumerable<Recipe> candidateRecipes);
    }
}
