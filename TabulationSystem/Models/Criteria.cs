using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Criteria
    {
        public int CriteriaId { get; set; }

        [Required]
        [StringLength(200)]
        public string CriteriaName { get; set; }

        [Range(0, 100)]
        public decimal Percentage { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public int ApplicationUserId { get; set; }

        // Navigation properties
        public ApplicationUser AdminUser { get; set; }
        public ICollection<Score> Scores { get; set; } = new List<Score>();

    }
}
