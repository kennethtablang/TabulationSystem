using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Criteria
    {
        public int CriteriaId { get; set; }

        [StringLength(200)]
        public string? CriteriaName { get; set; }

        [Range(0, 100)]
        [Precision(5, 2)]
        public decimal CriteriaPercentage { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // The maximum points a judge can give is now equal to the criteria percentage itself
        [NotMapped]
        public int MaximumPoints => (int)Math.Round(CriteriaPercentage);

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public string? ApplicationUserId { get; set; }

        //Foreign Key for EventCategory
        [Required]  //Ensures every Criteria belongs to an Event Category
        public int EventCategoryId { get; set; }

        //[Required]
        //public int CategoryId { get; set; }

        // Navigation properties
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? AdminUser { get; set; }

        [ForeignKey("CategoryId")]
        public EventCategory? Category { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
