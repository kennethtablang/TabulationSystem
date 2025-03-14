using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Data;
using TabulationSystem.Models;
using TabulationSystem.Models.ViewModels;

namespace TabulationSystem.Controllers
{
    public class CriteriaController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CriteriaController (ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Criteria/Index
        public IActionResult Index()
        {
            var criteriaList = _context.Criteria
                .Include(c => c.Category)
                .Select(c => new CriteriaViewModel
                {
                    CriteriaId = c.CriteriaId,
                    CriteriaName = c.CriteriaName,
                    Percentage = c.CriteriaPercentage,
                    Description = !string.IsNullOrWhiteSpace(c.Description) ? c.Description : "None", // Handle null/empty case
                    CategoryName = c.Category.CategoryName,
                    EventCategoryId = c.EventCategoryId
                })
                .ToList();

            return View(criteriaList);
        }

        // GET: CriteriaController/Create
        public ActionResult CreateCriteria()
        {
            ViewBag.EventCategories = new SelectList(_context.EventCategories, "EventCategoryId", "CategoryName");
            return View();
        }

        // POST: CriteriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCriteria(Criteria criteria)
        {
            if (ModelState.IsValid)
            {
                criteria.DateCreated = DateTime.UtcNow;
                criteria.DateUpdated = DateTime.UtcNow;

                _context.Criteria.Add(criteria);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown in case of error
            ViewData["EventCategoryId"] = new SelectList(_context.EventCategories, "EventCategoryId", "CategoryName", criteria.EventCategoryId);
            return View(criteria);
        }

        //// GET: CriteriaController/Edit/5
        //public IActionResult Edit(int id)
        //{
        //    var criteria = _context.Criteria.Find(id);
        //    if (criteria == null)
        //        return NotFound();

        //    var viewModel = new CriteriaViewModel
        //    {
        //        CriteriaId = criteria.CriteriaId,
        //        CriteriaName = criteria.CriteriaName,
        //        Percentage = criteria.CriteriaPercentage,
        //        Description = criteria.Description,
        //        EventCategoryId = criteria.EventCategoryId
        //    };

        //    ViewBag.EventCategories = new SelectList(_context.EventCategories, "EventCategoryId", "CategoryName", criteria.EventCategoryId);
        //    return View("EditCriteria", viewModel);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCriteria(CriteriaViewModel viewModel)
        {
            Console.WriteLine($"CriteriaId: {viewModel.CriteriaId}");
            Console.WriteLine($"CriteriaName: {viewModel.CriteriaName}");
            Console.WriteLine($"Percentage: {viewModel.Percentage}");
            Console.WriteLine($"Description: {viewModel.Description}");
            Console.WriteLine($"EventCategoryId: {viewModel.EventCategoryId}");

            if (ModelState.IsValid)
            {
                var criteria = _context.Criteria.Find(viewModel.CriteriaId);
                if (criteria == null)
                {
                    return Json(new { success = false, message = "Criteria not found." });
                }

                criteria.CriteriaName = viewModel.CriteriaName;
                criteria.CriteriaPercentage = viewModel.Percentage;
                criteria.Description = viewModel.Description;
                criteria.EventCategoryId = viewModel.EventCategoryId;
                criteria.DateUpdated = DateTime.UtcNow;

                _context.Update(criteria);
                _context.SaveChanges();

                // Return updated data as JSON for AJAX-based UI updates
                //var updatedCriteria = new
                return Json(new
                {
                    success = true,
                    message = "Criteria updated successfully.",
                    data = new
                    {
                        criteriaId = criteria.CriteriaId,
                        criteriaName = criteria.CriteriaName,
                        percentage = criteria.CriteriaPercentage,
                        description = criteria.Description,
                        categoryName = _context.EventCategories.Where(ec => ec.EventCategoryId == criteria.EventCategoryId)
                                        .Select(ec => ec.CategoryName).FirstOrDefault(),
                        eventCategoryId = criteria.EventCategoryId
                    }
                });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Invalid data submitted.", errors });
        }

        // GET: Criteria/GetCriteriaById/5
        // Returns JSON data for criteria, used by AJAX when updating the DataTable row.
        [HttpGet]
        public IActionResult GetCriteriaById(int id)
        {
            var criteria = _context.Criteria
                .Include(c => c.Category)
                .FirstOrDefault(c => c.CriteriaId == id);

            if (criteria == null)
                return NotFound();

            return Json(new
            {
                criteriaId = criteria.CriteriaId,
                criteriaName = criteria.CriteriaName,
                description = criteria.Description,
                percentage = criteria.CriteriaPercentage,
                categoryName = criteria.Category?.CategoryName,
                eventCategoryId = criteria.EventCategoryId
            });
        }

        // GET: CriteriaController/Delete/5
        // Returns a view (with Layout = null) to confirm deletion.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var criteria = _context.Criteria
                .Include(c => c.Category)
                .FirstOrDefault(c => c.CriteriaId == id);
            if (criteria == null)
                return NotFound();

            var viewModel = new CriteriaViewModel
            {
                CriteriaId = criteria.CriteriaId,
                CriteriaName = criteria.CriteriaName,
                Percentage = criteria.CriteriaPercentage,
                Description = criteria.Description,
                CategoryName = criteria.Category?.CategoryName,
                EventCategoryId = criteria.EventCategoryId
            };

            return View("DeleteCriteria", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var criteria = _context.Criteria.Find(id);
            if (criteria == null)
                return Json(new { success = false, message = "Criteria not found." });

            _context.Criteria.Remove(criteria);
            _context.SaveChanges();

            return Json(new { success = true, message = "Criteria deleted successfully." });
        }

    }
}