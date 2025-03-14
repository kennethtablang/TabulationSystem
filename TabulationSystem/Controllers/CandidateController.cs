using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TabulationSystem.Data;
using TabulationSystem.Models;
using System.IO;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace TabulationSystem.Controllers
{
    public class CandidateController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CandidateController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var candidates = _context.Candidates
                .Include(c => c.Event)   // include related event data if needed
                .ToList();
            return View(candidates);
        }

        // GET: Candidate/CreateCandidate
        [HttpGet]
        public IActionResult CreateCandidate()
        {
            // Populate the Events dropdown
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName");
            return View();
        }

        // POST: Candidate/CreateCandidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCandidate(Candidate candidate, IFormFile PictureFile)
        {
            if (ModelState.IsValid)
            {
                // Set the ApplicationUserId to the current logged-in user's ID
                candidate.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Process uploaded picture file if provided
                if (PictureFile != null && PictureFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await PictureFile.CopyToAsync(ms);
                        // Store image as a byte array
                        candidate.Picture = ms.ToArray();
                    }
                }

                candidate.DateCreated = DateTime.UtcNow;
                candidate.DateUpdated = DateTime.UtcNow;

                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Repopulate Events dropdown on error
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName", candidate.EventId);
            return View(candidate);
        }
    }
}
