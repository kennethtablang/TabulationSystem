namespace TabulationSystem.Models.ViewModels
{
    public class CandidateViewModel
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public int CandidateNumber { get; set; }
        public List<CategoryViewModel> EventCategories { get; set; } = new List<CategoryViewModel>(); // ✅ Add this!
    }
}
