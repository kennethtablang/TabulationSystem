using System.ComponentModel.DataAnnotations;

namespace TabulationSystem.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public DateTime? DateUpdated { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int EventId { get; set; }

        // Navigation property
        public Event? Event { get; set; }
    }
}
