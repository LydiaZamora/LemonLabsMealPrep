using LemonLabs.Web.Models;

namespace LemonLabs.Web.Services
{
    public class MealPlanResult
    {
        public List<Recipe> Meals { get; set; } = new();
        public int TotalCalories { get; set; }
        public decimal TotalCost { get; set; }
    }
}
