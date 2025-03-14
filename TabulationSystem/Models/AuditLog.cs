using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public enum SeverityLevel
    {
        Information,
        Warning,
        Error,
        Critical
    }

    public class AuditLog
    {
        public int AuditLogId { get; set; }

        [Required]
        public string? ApplicationUserId { get; set; } // Use string for IdentityUser FK

        [Required]
        [StringLength(500)]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Use UTC for consistency

        [StringLength(1000)]
        public string Details { get; set; }

        [StringLength(20)]
        public SeverityLevel Severity { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow; // Add this to fix the error

        // Navigation property
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; } // User who performed the action
    }
}
