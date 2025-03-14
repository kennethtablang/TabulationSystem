using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Data;
using TabulationSystem.Models;
using TabulationSystem.Models.ViewModels;

namespace TabulationSystem.Controllers
{
    public class EventCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateEventCategory()
        {
            ViewBag.Events = _context.Events.Any()
                ? new SelectList(_context.Events, "EventId", "EventName")
                : new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEventCategory(EventCategory eventCategory)
        {
            if (ModelState.IsValid)
            {
                _context.EventCategories.Add(eventCategory);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName");
            return View(eventCategory);
        }


        public IActionResult Index()
        {
            var categories = _context.EventCategories
                .Include(x => x.Event) // Fetch related Event data
                .Select(x => new EventCategoryViewModel
                {
                    EventCategoryId = x.EventCategoryId,
                    EventId = x.EventId, // Include EventId
                    EventName = x.Event != null ? x.Event.EventName : "N/A", // Handle null case
                    CategoryName = x.CategoryName,
                    Description = !string.IsNullOrWhiteSpace(x.Description) ? x.Description : "None", // Handle null/empty case
                    ScoreType = (ScoreType)x.ScoreType,// converting the nullable enum to string
                    CategoryPercentage = (decimal)x.CategoryPercentage // Convert double to decimal
                })
                .ToList();

            return View(categories);
        }

        //GET: EventCategory/GetCategoryById/5
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.EventCategories
                .Include(e => e.Event)
                .FirstOrDefault(c => c.EventCategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            // Return JSON data
            return Json(new
            {
                eventCategoryId = category.EventCategoryId,
                eventId = category.EventId,
                eventName = category.Event.EventName,
                categoryName = category.CategoryName,
                description = category.Description,
                scoreType = category.ScoreType.ToString(),
                categoryPercentage = category.CategoryPercentage
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EventCategory model)
        {
            if (id != model.EventCategoryId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();
                return Ok(new { success = true });
            }
            return BadRequest(ModelState);
        }

        //GET: EventCategory/EditEventCategory/5
        [HttpPost]
        public IActionResult EditEventCategory(EventCategory model)
        {
            if (ModelState.IsValid)
            {
                var eventCategory = _context.EventCategories.FirstOrDefault(x => x.EventCategoryId == model.EventCategoryId);

                if (eventCategory != null)
                {
                    eventCategory.EventId = model.EventId;
                    eventCategory.CategoryName = model.CategoryName;
                    eventCategory.Description = model.Description;
                    eventCategory.ScoreType = model.ScoreType;
                    eventCategory.CategoryPercentage = model.CategoryPercentage;

                    _context.SaveChanges();

                    //Return the updated data as JSON response
                    var updatedCategory = new
                    {
                        eventCategory.EventCategoryId,
                        EventName = _context.Events
                                        .Where(e => e.EventId == model.EventId)
                                        .Select(e => e.EventName)
                                        .FirstOrDefault(),
                        eventCategory.CategoryName,
                        eventCategory.ScoreType,
                        eventCategory.CategoryPercentage
                    };

                    return Json(new
                    {
                        success = true,
                        message = "Event Category updated successfully!",
                        data = updatedCategory
                    });
                }

                return Json(new { success = false, message = "Event Category not found!" });
            }

            return Json(new { success = false, message = "Invalid data submitted!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var eventCategory = _context.EventCategories.Find(id);
            if (eventCategory == null)
                return NotFound();

            _context.EventCategories.Remove(eventCategory);
            _context.SaveChanges();
            return Ok(new { success = true });
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var eventCategories = _context.EventCategories
                .Select(e => new
                {
                    e.EventCategoryId,
                    e.CategoryName
                })
                .ToList();

            return Json(eventCategories);
        }
    }
}
