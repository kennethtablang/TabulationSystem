using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabulationSystem.Models
{
    public enum ScoreType
    {
        None,
        Percentage,
        Points,
        Rank
    }

    public class EventCategory
    {
        public int EventCategoryId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public ScoreType ScoreType { get; set; }

        [Range(0, 100)]
        public double CategoryPercentage { get; set; } //percentage contribution to total event score

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        // Navigation property for the related Event
        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        // Navigation property for related Scores
        public ICollection<Score> Scores { get; set; } = new List<Score>();

        // One EventCategory has Many Criteria
        public ICollection<Criteria> Criteria { get; set; } = new List<Criteria>();
    }
}
