namespace TabulationSystem.Models.ViewModels
{
    public class LiveResultsViewModel
    {
        public List<CandidateResultViewModel> Candidates { get; set; } = new List<CandidateResultViewModel>();
    }

    public class CandidateResultViewModel
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public decimal Score { get; set; }
        public int Rank { get; set; }
    }

}
