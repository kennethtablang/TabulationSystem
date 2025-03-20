namespace TabulationSystem.Models.ViewModels
{
    public class JudgeDashboardViewModel
    {
        public int EventId { get; set; } // Add this line
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<CandidateViewModel> Candidates { get; set; } = new List<CandidateViewModel>();
        public List<CriteriaScoreViewModel> Scores { get; set; } = new List<CriteriaScoreViewModel>();
    }

}
