using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public enum EventStatus
    {
        Upcoming,
        Ongoing,
        Ended
    }

    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public EventStatus EventStatus { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }  // Added Event Date

        [StringLength(255)]
        public string? EventLocation { get; set; }  // Added Event Location

        [NotMapped]
        public IFormFile? EventBanner { get; set; }  // Added Event Banner as File

        [StringLength(500)]
        public string? Banner { get; set; }  // This will store the uploaded file path

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public string? ApplicationUserId { get; set; }

        // Navigation properties
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? Admin { get; set; }

        public ICollection<EventAssignment> EventAssignments { get; set; } = new List<EventAssignment>();

        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();

        public ICollection<Score> Scores { get; set; } = new List<Score>();

        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
