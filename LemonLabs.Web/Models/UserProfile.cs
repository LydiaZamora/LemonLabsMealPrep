using System.ComponentModel.DataAnnotations;

namespace LemonLabs.Web.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        public DietType DietType { get; set; }

        [StringLength(300)]
        public string? Allergies { get; set; }

        [Range(500, 6000)]
        public int DailyCalorieGoal { get; set; } = 2000;

        [Range(0, 10000)]
        public decimal WeeklyBudget { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
