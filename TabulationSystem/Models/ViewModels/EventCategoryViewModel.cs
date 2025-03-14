namespace TabulationSystem.Models.ViewModels
{
    public class EventCategoryViewModel
    {
        public int EventCategoryId { get; set; }
        public string EventName { get; set; }  // <-- This will hold the EventName
        public int EventId { get; set; }  // <-- Add this line
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ScoreType ScoreType { get; set; }
        public decimal CategoryPercentage { get; set; }
    }
}
