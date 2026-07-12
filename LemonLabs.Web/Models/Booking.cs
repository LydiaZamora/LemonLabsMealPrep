using System.ComponentModel.DataAnnotations;

namespace LemonLabs.Web.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }

        public int ChefId { get; set; }
        public Chef? Chef { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestedDate { get; set; } = DateTime.Today.AddDays(3);

        public BookingStatus Status { get; set; } = BookingStatus.Requested;

        [StringLength(300)]
        public string? Notes { get; set; }
    }
}
