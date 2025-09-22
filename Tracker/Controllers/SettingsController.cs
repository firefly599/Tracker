using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracker.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly string _dataFile = Path.Combine("Data", "forms.json");

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Submit()
        {
            // Placeholder for future settings submission logic
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Purge()
        {
            if (System.IO.File.Exists(_dataFile))
            {
                System.IO.File.Delete(_dataFile);
            }

            TempData["Message"] = "All form data has been deleted.";

            return RedirectToAction("Index", "History");
        }
    }
}
