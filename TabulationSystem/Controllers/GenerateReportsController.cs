using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TabulationSystem.Controllers
{
    public class GenerateReportsController : Controller
    {
        // GET: GenerateReportController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GenerateReportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GenerateReportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenerateReportController/Create
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

        // GET: GenerateReportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GenerateReportController/Edit/5
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

        // GET: GenerateReportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerateReportController/Delete/5
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
