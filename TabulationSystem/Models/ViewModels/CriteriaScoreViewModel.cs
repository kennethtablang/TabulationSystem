namespace TabulationSystem.Models.ViewModels
{
    public class CriteriaScoreViewModel
    {
        public int? CriteriaId { get; set; }
        public string? CriteriaName { get; set; }

        public int? CandidateId { get; set; }
        public string? CandidateName { get; set; }

        public int JudgeId { get; set; }
        public string? JudgeName { get; set; } // Optional, for display purposes

        public decimal? Score { get; set; } // The score the judge gives
        public int MaxScore { get; set; } // Ensures judge cannot exceed max
    }
}
