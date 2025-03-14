using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Candidate
    {

        public Candidate()
        {
            YearId = 2025;
            Gender = Gender.Female;
        }

        public int CandidateId { get; set; }

        [Required]
        [Range(1, 100)]
        public int CandidateNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {(MiddleName ?? "").Trim()} {LastName}".Trim();

        [Required]
        public Gender Gender { get; set; }

        public bool? Status { get; set; } = true;

        // New properties for image handling
        public byte[]? Picture { get; set; } // Store image as binary data

        [NotMapped]
        public IFormFile? PictureFile { get; set; } // Handle file uploads in the form

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public int? YearId { get; set; }

        [Required]
        public int EventId { get; set; }

        public string? ApplicationUserId { get; set; }

        // Navigation properties
        [ForeignKey("YearId")]
        public Year? Year { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? AdminUser { get; set; }

        [ForeignKey("EventId")]
        public Event? Event { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}