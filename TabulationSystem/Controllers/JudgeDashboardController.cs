using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TabulationSystem.Data;
using TabulationSystem.Models.ViewModels;

namespace TabulationSystem.Controllers
{
    public class JudgeDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JudgeDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JudgeDashboard/Index
        public async Task<IActionResult> Index()
        {
            var candidates = await _context.Candidates
                .Select(c => new CandidateViewModel
                {
                    CandidateId = c.CandidateId,
                    FirstName = c.FirstName,
                    PhotoPath = c.Picture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(c.Picture)}" : "/Images/default-profile.png",
                    Description = c.Description,
                    CandidateNumber = c.CandidateNumber
                })
                .ToListAsync();

            var events = await _context.Events
                .Select(e => new EventViewModel
                {
                    EventId = e.EventId,
                    EventName = e.EventName
                })
                .ToListAsync();

            var viewModel = new JudgeDashboardViewModel
            {
                Candidates = candidates,
                Events = events
            };

            return View(viewModel);
        }


        // GET: JudgeDashboard/LiveResult ----> this is a  test for the Live Scoring
        public async Task<IActionResult> LiveResult()
        {
            return View();
        }

        // GET: JudgeDashboard/EnterScores/{eventId}
        public async Task<IActionResult> EnterScores(int eventId)
        {
            // Check if the event exists
            var eventDetails = await _context.Events
                .Where(e => e.EventId == eventId)
                .Select(e => new { e.EventId, e.EventName })
                .FirstOrDefaultAsync();

            if (eventDetails == null)
            {
                return NotFound("Event not found.");
            }

            // Get the logged-in judge's ID
            var judgeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get categories and their criteria for the event
            var categories = await _context.EventCategories
                .Where(ec => ec.EventId == eventId)
                .Include(ec => ec.Criteria) // Include criteria in the same query
                .Select(ec => new CategoryViewModel
                {
                    EventCategoryId = ec.EventCategoryId,
                    CategoryName = ec.CategoryName,
                    Criteria = ec.Criteria.Select(c => new CriteriaViewModel
                    {
                        CriteriaId = c.CriteriaId,
                        CriteriaName = c.CriteriaName,
                        Percentage = c.CriteriaPercentage,
                        MaxScore = c.MaximumPoints
                    }).ToList()
                }).ToListAsync();

            // Get candidates participating in the event
            var candidates = await _context.Candidates
                .Where(c => c.EventId == eventId)
                .Select(c => new CandidateViewModel
                {
                    CandidateId = c.CandidateId,
                    CandidateNumber = c.CandidateNumber,
                    FullName = c.FullName,
                    PhotoPath = c.Picture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(c.Picture)}" : "/Images/default-profile.png"
                }).ToListAsync();

            // Fetch existing scores by the judge
            var scores = await _context.Scores
                .Where(s => s.JudgeId == judgeId && s.EventId == eventId)
                .Select(s => new CriteriaScoreViewModel
                {
                    CandidateId = s.CandidateId ?? 0,
                    CriteriaId = s.CriteriaId ?? 0,
                    Score = s.ScoreValue
                }).ToListAsync();

            var model = new EnterScoresViewModel
            {
                EventId = eventDetails.EventId,
                EventName = eventDetails.EventName,
                Categories = categories,
                Candidates = candidates,
                Scores = scores // Now contains existing scores instead of an empty list
            };

            return View(model);
        }


        // GET: JudgeDashboard/FinalRankings/{eventId}
        public async Task<IActionResult> FinalRankings(int eventId)
        {
            return View();
        }
    }
}
