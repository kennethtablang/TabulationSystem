using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabulationSystem.Models
{
    public class Year
    {
        public int YearId { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int YearNumber { get; set; } = 2025;

        public bool Status { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
