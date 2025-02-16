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
        public int CandidateId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public bool Status { get; set; } = true;

        [NotMapped]
        public IFormFile Picture { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public int YearId { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        // Navigation properties
        public Year Year { get; set; }
        public ApplicationUser AdminUser { get; set; }
        public ICollection<Score> Scores { get; set; }
        //public object Years { get; internal set; }

    }
}
