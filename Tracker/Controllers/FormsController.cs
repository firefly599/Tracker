using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class FormsController : Controller
    {
        private readonly string _dataFile = Path.Combine("Data", "forms.json");


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Submit(string name, int age)
        {
            var formData = new FormData
            {
                Name = name,
                Age = age
            };

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

            data.Add(formData);

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_dataFile, json);


            return RedirectToAction("Index", "View");
        }
    }
}
