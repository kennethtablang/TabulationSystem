using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Models
{
    public class EventAssignment
    {
        [Key]
        public int EventAssignmentId { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public int? EventId { get; set; }

        public DateTime DateAssigned { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
