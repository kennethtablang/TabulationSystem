using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TabulationSystem.Controllers
{
    public class AuditLogsController : Controller
    {
        // GET: AuditLogController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuditLogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuditLogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditLogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuditLogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuditLogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuditLogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuditLogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
