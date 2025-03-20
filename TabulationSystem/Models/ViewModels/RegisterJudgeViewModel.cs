using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TabulationSystem.Models.ViewModels
{
    public class RegisterJudgeViewModel
    {
        [Required(ErrorMessage = "The First Name is required.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Optional: Judge may be assigned an event at registration
        public int? EventId { get; set; }

        // This will populate the dropdown list of events
        public IEnumerable<SelectListItem> Events { get; set; } = new List<SelectListItem>();
    }
}
