using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Data;
using System.Threading.Tasks;
using TabulationSystem.Models;
using TabulationSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TabulationSystem.Areas.Identity.Data;

namespace TabulationSystem.Controllers
{
    [Route("ManageUser")]
    public class ManageUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //POST ManageUser/AssignEvent/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AssignEvent")]
        public async Task<IActionResult> AssignEvent(EventAssignment assignment)
        {
            if (ModelState.IsValid)
            {
                assignment.DateAssigned = DateTime.UtcNow;
                _context.EventAssignments.Add(assignment);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Judge successfully assigned to the event!" });
            }
            return Json(new { success = false, message = "Invalid data submitted." });
        }

        //  GET: ManageUser/Judge
        [Route("Judge")]
        public async Task<IActionResult> Judge()
        {
            //  Fetch the list of judges from AspNetUsers table
            var judges = await (from user in _context.Users
                                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                join role in _context.Roles on userRole.RoleId equals role.Id
                                where role.Name == "Judge"
                                select new JudgeAssignmentViewModel
                                {
                                    UserId = user.Id,
                                    FirstName = user.FirstName,
                                    MiddleName = user.MiddleName,
                                    LastName = user.LastName
                                })
                                .ToListAsync();

            //  Fetch the list of upcoming events
            var events = await _context.Events
                .Where(e => e.EventStatus == EventStatus.Upcoming)
                .Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.EventName
                })
                .ToListAsync();

            //  Create the ViewModel
            var viewModel = new EventAssignmentViewModel
            {
                Events = events,
                JudgeAssignments = judges
            };

            return View(viewModel);
        }

        //  GET: ManageUser/Manager
        [Route("Manager")]
        public async Task<IActionResult> Manager()
        {
            var managers = await (from user in _context.Users
                                  join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.Id
                                  where role.Name == "Manager"
                                  select user).ToListAsync();

            return View(managers);
        }

        //  GET: ManageUser/Admin
        [Route("Admin")]
        public async Task<IActionResult> Admin()
        {
            var admins = await (from user in _context.Users
                                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                join role in _context.Roles on userRole.RoleId equals role.Id
                                where role.Name == "Admin"
                                select user).ToListAsync();

            return View(admins);
        }

        // GET: ManageUser/RegisterJudge
        [Route("RegisterJudge")]
        [HttpGet]
        public async Task<IActionResult> RegisterJudge()
        {
            // Populate the events dropdown with upcoming events
            var events = await _context.Events
                .Where(e => e.EventStatus == EventStatus.Upcoming)
                .Select(e => new SelectListItem
                {
                    Value = e.EventId.ToString(),
                    Text = e.EventName
                }).ToListAsync();

            var viewModel = new RegisterJudgeViewModel
            {
                Events = events
            };

            return View(viewModel);
        }

        // POST: ManageUser/RegisterJudge
        [Route("RegisterJudge")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterJudge(RegisterJudgeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Create a new judge user
                var user = new ApplicationUser
                {
                    FirstName = viewModel.FirstName,
                    MiddleName = viewModel.MiddleName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    UserName = viewModel.Email,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    // Assign the Judge role
                    await _userManager.AddToRoleAsync(user, "Judge");

                    // If an event is selected, create an EventAssignment record (optional)
                    if (viewModel.EventId.HasValue)
                    {
                        var assignment = new EventAssignment
                        {
                            EventId = viewModel.EventId.Value,
                            UserId = user.Id,
                            DateAssigned = DateTime.UtcNow
                        };
                        _context.EventAssignments.Add(assignment);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction("Judge");
                }

                // Add errors to ModelState if creation fails
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If model state is invalid, repopulate events dropdown and return view
            viewModel.Events = new SelectList(await _context.Events
                .Where(e => e.EventStatus == EventStatus.Upcoming)
                .ToListAsync(), "EventId", "EventName", viewModel.EventId);

            return View(viewModel);
        }
    }
}
