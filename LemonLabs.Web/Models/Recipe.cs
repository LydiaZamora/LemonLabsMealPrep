using System.ComponentModel.DataAnnotations;

namespace LemonLabs.Web.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DietType DietType { get; set; }

        public MealCategory Category { get; set; }

        [Range(0, 5000)]
        public int Calories { get; set; }

        [Range(0, 10000)]
        public decimal PricePerServing { get; set; }

        [StringLength(300)]
        public string? Allergens { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
