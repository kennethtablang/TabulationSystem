using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Action { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [StringLength(1000)]
        public string Details { get; set; }

        [StringLength(20)]
        public string Severity { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } // User who performed the action

    }
}
