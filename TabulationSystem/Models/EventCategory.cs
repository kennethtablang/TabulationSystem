using System.ComponentModel.DataAnnotations;

namespace TabulationSystem.Models
{
    public class EventCategory
    {
        public int EventCategoryId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string ScoreType { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        // Navigation property for the related Event
        public Event Event { get; set; }

        //Navigation property for the related Scores
        public ICollection<Score> Scores { get; set; } = new List<Score>();

    }
}
