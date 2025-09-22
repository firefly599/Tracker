using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Authorize]
    public class FormsController : Controller
    {
        private readonly string _dataFile = Path.Combine("Data", "forms.json");


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(FormData formData)
        {
            if (formData == null)
            {
                return BadRequest("Form data is null.");
            }

            // Ensure the Data directory exists
            Directory.CreateDirectory("Data");

            // Loads existing data
            List<FormData> data = new();
            if (System.IO.File.Exists(_dataFile))
            {
                string exists = System.IO.File.ReadAllText(_dataFile);
                if (!string.IsNullOrEmpty(exists))
                {
                    data = JsonSerializer.Deserialize<List<FormData>>(exists) ?? new List<FormData>();
                }
            }

            // Add new form data
            data.Add(formData);

            // Save updated data back to the file
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_dataFile, json);

            // Redirect to History page to view all submissions
            return RedirectToAction("Index", "History");
        }
    }
}
