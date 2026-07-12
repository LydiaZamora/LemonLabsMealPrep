using System.ComponentModel.DataAnnotations;

namespace LemonLabs.Web.Models
{
    public class Farm
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
