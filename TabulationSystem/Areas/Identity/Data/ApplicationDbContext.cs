using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Areas.Identity.Data;
using TabulationSystem.Models;

namespace TabulationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables in the database
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<EventAssignment> EventAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================
            //   Candidate → Event
            // =====================
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Candidates)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Score → Event
            // =====================
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Scores)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Score → Criteria
            // =====================
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Criteria)
                .WithMany(c => c.Scores)
                .HasForeignKey(s => s.CriteriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Score → Judge
            // =====================
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Judge)
                .WithMany()
                .HasForeignKey(s => s.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Score → Candidate
            // =====================
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Candidate)
                .WithMany(c => c.Scores)
                .HasForeignKey(s => s.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Score → EventCategory
            // =====================
            modelBuilder.Entity<Score>()
                .HasOne(s => s.Category)
                .WithMany(ec => ec.Scores)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Criteria → EventCategory
            // =====================
            modelBuilder.Entity<Criteria>()
                .HasOne(cr => cr.Category)
                .WithMany(cat => cat.Criteria)
                .HasForeignKey(cr => cr.EventCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   EventCategory → Event
            // =====================
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   Notification → User
            // =====================
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================
            //   AuditLog Configuration
            // =====================
            modelBuilder.Entity<AuditLog>()
                .Property(a => a.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            // =====================
            //   Score Table Configuration
            // =====================
            modelBuilder.Entity<Score>()
                .Property(s => s.ScoreValue)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Score>()
                .Property(s => s.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Score>()
                .Property(s => s.DateUpdated)
                .HasDefaultValueSql("GETUTCDATE()");

            // =====================
            // ✅ Added Composite Key in Scores to prevent duplicates
            // =====================
            modelBuilder.Entity<Score>()
                .HasKey(s => new { s.CandidateId, s.JudgeId, s.CriteriaId });

            modelBuilder.Entity<Score>()
                .HasIndex(s => new { s.CandidateId, s.JudgeId, s.CriteriaId })
                .IsUnique();

            // =====================
            //   EventAssignment Configuration
            // =====================
            modelBuilder.Entity<EventAssignment>()
                .HasKey(ea => new { ea.EventId, ea.UserId });

            modelBuilder.Entity<EventAssignment>()
                .HasOne(ea => ea.Event)
                .WithMany(e => e.EventAssignments)
                .HasForeignKey(ea => ea.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventAssignment>()
                .HasOne(ea => ea.User)
                .WithMany()
                .HasForeignKey(ea => ea.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventAssignment>()
                .HasIndex(ea => new { ea.EventId, ea.UserId })
                .IsUnique()
                .HasDatabaseName("IX_EventAssignment_UniqueAssignment");

            // Seed a Year record for 2025
            modelBuilder.Entity<Year>().HasData(
                new Year
                {
                    YearId = 2025,
                    YearNumber = 2025,
                    Status = true,
                    DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateUpdated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
