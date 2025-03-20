namespace TabulationSystem.Models.ViewModels
{
    public class CriteriaViewModel
    {
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public string? Description { get; set; }
        public decimal Percentage { get; set; }
        public string CategoryName { get; set; }
        public int EventCategoryId { get; set; }
        public int MaxScore { get; set; }
    }
}
