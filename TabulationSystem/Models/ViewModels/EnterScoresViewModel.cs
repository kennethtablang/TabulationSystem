namespace TabulationSystem.Models.ViewModels
{
    public class EnterScoresViewModel
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }

        public List<CandidateViewModel> Candidates { get; set; } = new List<CandidateViewModel>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        //List to hold scores per candidate per criterion
        public List<CriteriaScoreViewModel> Scores { get; set; } = new List<CriteriaScoreViewModel>();
    }

    public class FinalRankingsViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public List<CandidateViewModel> Candidates { get; set; } = new List<CandidateViewModel>();
    }

    public class CategoryViewModel
    {
        public int EventCategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<CriteriaViewModel> Criteria { get; set; } = new List<CriteriaViewModel>();
    }


}
