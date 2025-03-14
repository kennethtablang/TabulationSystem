using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TabulationSystem.Models;

namespace TabulationSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Display(Name = "Full Name")]
    public string FullName
    {
        get
        {
            return string.IsNullOrWhiteSpace(MiddleName)
                ? $"{LastName}, {FirstName}"
                : $"{LastName}, {FirstName} {MiddleName}";
        }
    }


    public bool? IsActive { get; set; } = true; // Soft delete functionality

    public byte[]? ProfilePicture { get; set; }

    /// <summary>
    /// Converts the ProfilePicture to Base64 string for display.
    /// </summary>
    public string? GetProfilePictureBase64()
    {
        if (ProfilePicture == null || ProfilePicture.Length == 0)
            return "/images/default-profile.png"; // Use a default profile image

        return $"data:image/png;base64,{Convert.ToBase64String(ProfilePicture)}";
    }

    public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateTimeUpdated { get; set; } = DateTime.UtcNow;

    public ICollection<Score> Scores { get; set; } = new List<Score>();

    public ICollection<EventAssignment> EventAssignments { get; set; } = new List<EventAssignment>();
}