using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Data;
using TabulationSystem.Models;
using TabulationSystem.Models.ViewModels;

namespace TabulationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ScoringController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoringController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ScoringController (Live Results)
        // This action serves as the Live Results page.
        public async Task<IActionResult> Index()
        {
            // Retrieve scores including candidate details, ordered by score descending.
            var scores = await _context.Scores
                .Include(s => s.Candidate)
                .OrderByDescending(s => s.ScoreValue)
                .ToListAsync();

            // Create a new LiveResultsViewModel
            var liveResults = new LiveResultsViewModel();

            // If you want to compute a rank, you can sort and then assign rank values.
            // Here, we assume the scores are already sorted descending.
            int rank = 1;
            foreach (var score in scores)
            {
                // Ensure Candidate is not null.
                if (score.Candidate != null)
                {
                    liveResults.Candidates.Add(new CandidateResultViewModel
                    {
                        CandidateId = score.Candidate.CandidateId,
                        FullName = score.Candidate.FullName,
                        Score = score.ScoreValue,
                        Rank = rank++
                    });
                }
            }

            return View(liveResults);
        }


        // GET: ScoringController/EnterScores
        public IActionResult EnterScores()
        {
            // Return the view for entering scores.
            // You can add logic here to populate dropdowns for candidates, criteria, etc.
            return View();
        }

        // GET: ScoringController/FinalRankings
        public async Task<IActionResult> FinalRankings()
        {
            // For demonstration, sort scores descending.
            // You might compute total scores per candidate and then assign ranks.
            var finalRankings = await _context.Scores
                .Include(s => s.Candidate)
                .OrderByDescending(s => s.ScoreValue)
                .ToListAsync();
            return View(finalRankings);
        }

        // GET: ScoringController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var score = await _context.Scores
                .Include(s => s.Candidate)
                .FirstOrDefaultAsync(s => s.ScoreId == id);
            if (score == null)
            {
                return NotFound();
            }
            return View(score);
        }

        // GET: ScoringController/Create
        public IActionResult Create()
        {
            // Return a view to create a new score.
            // Optionally populate dropdowns (e.g., for candidates and criteria) using ViewBag.
            return View();
        }

        // POST: ScoringController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Score score)
        {
            if (ModelState.IsValid)
            {
                score.DateCreated = DateTime.UtcNow;
                score.DateUpdated = DateTime.UtcNow;
                _context.Scores.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(score);
        }

        // GET: ScoringController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            return View(score);
        }

        // POST: ScoringController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Score score)
        {
            if (id != score.ScoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    score.DateUpdated = DateTime.UtcNow;
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Scores.Any(s => s.ScoreId == score.ScoreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(score);
        }

        // GET: ScoringController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            return View(score);
        }

        // POST: ScoringController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score != null)
            {
                _context.Scores.Remove(score);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
