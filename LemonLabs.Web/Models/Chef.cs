using System.ComponentModel.DataAnnotations;

namespace LemonLabs.Web.Models
{
    public class Chef
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Bio { get; set; }

        public DietType Specialty { get; set; }

        [Range(0, 50)]
        public int YearsExperience { get; set; }

        [Range(0, 1000)]
        public decimal HourlyRate { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
