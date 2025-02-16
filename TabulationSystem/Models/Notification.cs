using System.ComponentModel.DataAnnotations;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        public int ApplicationUserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool IsRead { get; set; }

        [StringLength(50)]
        public string NotificationType { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }

    }
}
