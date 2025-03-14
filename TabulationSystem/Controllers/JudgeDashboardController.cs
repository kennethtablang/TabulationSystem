using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TabulationSystem.Controllers
{
    public class JudgeDashboardController : Controller
    {
        // GET: JudgeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: JudgeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JudgeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JudgeController/Create
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

        // GET: JudgeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JudgeController/Edit/5
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

        // GET: JudgeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JudgeController/Delete/5
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
