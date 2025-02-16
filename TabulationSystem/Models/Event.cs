using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public int ApplicationUserId { get; set; }

        // Navigation properties
        public ApplicationUser AdminUser { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
        public ICollection<Score> Scores { get; set; } = new List<Score>();

    }
}
