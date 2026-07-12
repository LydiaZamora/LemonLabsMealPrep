using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LemonLabs.Web.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsOrganic { get; set; }

        [StringLength(100)]
        public string? SeasonalAvailability { get; set; }

        public int FarmId { get; set; }
        public Farm? Farm { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
