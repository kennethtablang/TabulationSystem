using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabulationSystem.Data;
using TabulationSystem.Models;

namespace TabulationSystem.Controllers
{
    public class ManageEventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ManageEventController> _logger;

        public ManageEventController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<ManageEventController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _context.Events
                .Select(e => new
                {
                    e.EventId,
                    e.EventName
                })
                .ToList();

            return Json(events);
        }


        // Helper method to handle file upload logic
        private async Task<string> HandleFileUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; // Return null if no file is uploaded
            }

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "events");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
            string fullPath = Path.Combine(filePath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/images/events/" + fileName;
        }

        // GET: ManageEventController
        public ActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events ?? new List<Event>());
        }

        // GET: ManageEventController/ArchivedEvent
        public ActionResult ArchivedEvent()
        {
            return View();
        }

        // GET: ManageEventController/Create
        public ActionResult CreateEvent()
        {
            return View();
        }

        // POST: ManageEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEvent(Event model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle banner file upload (moved to a helper method)
                    model.Banner = await HandleFileUpload(model.EventBanner);

                    model.DateCreated = DateTime.UtcNow;
                    model.DateUpdated = DateTime.UtcNow;
                    _context.Add(model);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating the event");
                    ModelState.AddModelError("", "An error occurred while saving the event. Please try again.");
                    return View(model);
                }
            }
            return View(model);
        }


        // GET: ManageEventController/Edit/5
        public ActionResult Edit(int id)
        {
            var eventToEdit = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (eventToEdit == null)
            {
                return NotFound();
            }

            return View(eventToEdit);
        }


        // POST: ManageEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Event model)
        {
            if (id != model.EventId)
            {
                Console.WriteLine("Not found triggered");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle banner file upload (moved to a helper method)
                    if (model.EventBanner != null && model.EventBanner.Length > 0)
                    {
                        model.Banner = await HandleFileUpload(model.EventBanner);
                    }

                    //update the DateUpdated field
                    model.DateUpdated = DateTime.UtcNow;

                    //Update the event in the database
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while editing the event");
                    ModelState.AddModelError("", "An error occurred while saving the event. Please try again.");
                    return View(model);
                }
            }
            // If model state is invalid, return to the edit view with the current model
            return View(model);
        }

        // GET: ManageEventController/Delete/5
        public ActionResult Delete(int id)
        {
            var eventToDelete = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            return View(eventToDelete);
        }

        // POST: ManageEventController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                // Optionally, delete the banner image from the file system
                if (!string.IsNullOrEmpty(eventToDelete.Banner))
                {
                    string bannerPath = Path.Combine(_webHostEnvironment.WebRootPath, eventToDelete.Banner.TrimStart('/'));
                    if (System.IO.File.Exists(bannerPath))
                    {
                        System.IO.File.Delete(bannerPath);
                    }
                }

                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
