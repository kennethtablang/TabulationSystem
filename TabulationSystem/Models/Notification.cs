using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public enum NotificationType
    {
        System,
        EventUpdate,
        UserMessage
    }

    public class Notification
    {
        public int NotificationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } // Keep this as the FK

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;

        [Required]
        public NotificationType NotificationType { get; set; }

        // Navigation property
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? User { get; set; }
    }
}
