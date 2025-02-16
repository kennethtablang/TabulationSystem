using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int EventId { get; set; }
        public int CriteriaId { get; set; }
        public int JudgeId { get; set; }
        public int CandidateId { get; set; }
        public int CategoryId { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public Criteria Criteria { get; set; }
        public ApplicationUser Judge { get; set; }
        public Candidate Candidate { get; set; }
        public EventCategory Category { get; set; }

        [Range(0, 100)]
        public decimal ScoreValue { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

    }
}
