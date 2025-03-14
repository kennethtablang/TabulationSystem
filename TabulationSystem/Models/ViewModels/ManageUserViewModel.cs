using Microsoft.AspNetCore.Mvc.Rendering;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models.ViewModels
{
    public class ManageUserViewModel
    {
        // Contains the judge assignment data (judge list and events dropdown)
        public EventAssignmentViewModel EventAssignment { get; set; } = new EventAssignmentViewModel();

        public List<JudgeAssignmentViewModel> JudgeAssignments
        {
            get { return EventAssignment.JudgeAssignments; }
        }

        public List<SelectListItem> Events
        {
            get { return EventAssignment.Events; }
            set { EventAssignment.Events = value; }
        }


        // Optional: List of roles (if you plan to assign or filter by roles)
        public IEnumerable<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

        // Optional: A collection of ApplicationUser objects (e.g., all judges or users)
        public IEnumerable<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        // Optional: Additional properties for filtering/searching or paging
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
