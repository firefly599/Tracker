using Microsoft.AspNetCore.Mvc;

namespace Tracker.Controllers
{
    public class History : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
