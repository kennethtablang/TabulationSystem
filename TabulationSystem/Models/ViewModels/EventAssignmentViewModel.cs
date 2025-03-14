using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TabulationSystem.Models.ViewModels
{
    public class EventAssignmentViewModel
    {
        // This holds the list of assignments to display in the table
        public List<SelectListItem> Events { get; set; } = new List<SelectListItem>();
        public List<JudgeAssignmentViewModel> JudgeAssignments { get; set; } = new List<JudgeAssignmentViewModel>();
    }

    public class JudgeAssignmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                // Handle null MiddleName without extra space
                return string.IsNullOrWhiteSpace(MiddleName)
                    ? $"{LastName}, {FirstName}"
                    : $"{LastName}, {FirstName} {MiddleName}";
            }
        }

        [Display(Name = "Assigned Event")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Needed for assigning events (hidden in modal)
        public string UserId { get; set; }
    }

    public class EventDropdownViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
