using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Score
    {
        public int? ScoreId { get; set; }

        [Required]
        public int? EventId { get; set; }

        [Required]
        public int? CriteriaId { get; set; }

        [Required]
        public string? JudgeId { get; set; }

        [Required]
        public int? CandidateId { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        [ForeignKey("CriteriaId")]
        public Criteria? Criteria { get; set; }

        [ForeignKey("JudgeId")]
        public ApplicationUser? Judge { get; set; }

        [ForeignKey("CandidateId")]
        public Candidate? Candidate { get; set; }

        [ForeignKey("CategoryId")]
        public EventCategory? Category { get; set; }

        [Required]
        [Range(0, 100)]
        [Column(TypeName = "decimal(5,2)")] // Explicitly define precision
        public decimal ScoreValue { get; set; }

        public bool IsFinalized { get; set; } = false;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}
