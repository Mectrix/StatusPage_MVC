using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StatusPage_MVC.Controllers
{
    public class CMDBController : Controller
    {
        // GET: CMDBController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CMDBController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CMDBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CMDBController/Create
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

        // GET: CMDBController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CMDBController/Edit/5
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

        // GET: CMDBController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CMDBController/Delete/5
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
