using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        // This is a duplicated line in both Forms/History controllers
        // Look into refactoring this into a shared service or base class
        private readonly string _dataFile = Path.Combine("Data", "forms.json");

        public IActionResult Index()
        {
            List<FormData> data = new();

            // Duplicate code block for reading JSON file in Forms/History controllers
            if (System.IO.File.Exists(_dataFile))
            {
                // Duplicate line for reading file content in Forms/History controllers
                string json = System.IO.File.ReadAllText(_dataFile); ;
                if (!string.IsNullOrWhiteSpace(json))
                {
                    data = JsonSerializer.Deserialize<List<FormData>>(json) ?? new List<FormData>();
                }
            }

            return View(data);
        }

        public IActionResult PopulateDummy(int count = 5)
        {
            Directory.CreateDirectory("Data");

            List<FormData> data = new();
            if (System.IO.File.Exists(_dataFile))
            {
                string exists = System.IO.File.ReadAllText(_dataFile);
                if (!string.IsNullOrEmpty(exists))
                {
                    data = JsonSerializer.Deserialize<List<FormData>>(exists) ?? new List<FormData>();
                }
            }

            var random = new Random();

            var names = new[] { "Alice", "Bob", "Charlie", "Diana", "Ethan", "Fiona", "George", "Hannah" };
            var surnames = new[] { "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White" };
            for (int i = 0; i < count; i++)
            {
                var dummy = new FormData
                {
                    Name = names[random.Next(names.Length)] + " " + surnames[random.Next(surnames.Length)],
                    Age = random.Next(18, 100)
                };
                data.Add(dummy);
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_dataFile, json);

            return RedirectToAction("Index");
        }
    }
}
