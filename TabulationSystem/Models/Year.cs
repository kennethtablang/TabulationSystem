using System.ComponentModel.DataAnnotations;

namespace TabulationSystem.Models
{
    public class Year
    {
        public int YearId { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string YearNumber { get; set; }
        public bool Status { get; set; }

        // Navigation properties
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    }
}
