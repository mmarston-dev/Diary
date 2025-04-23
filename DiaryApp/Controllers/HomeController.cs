using System.Diagnostics;
using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DiaryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<DiaryEntry> entries = _db.DiaryEntries.ToList();
            return View(entries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiaryEntry entry)
        {
            // Client side validation
            if (entry.Title.IsNullOrEmpty())
            {
                ModelState.AddModelError("Title", "Title is required");
            }
            if (entry.Content.IsNullOrEmpty())
            {
                ModelState.AddModelError("Content", "Content is required");
            }

            if (ModelState.IsValid)
            {
                // Update database
                _db.DiaryEntries.Add(entry);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Return error message
                return View(entry);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var entry = _db.DiaryEntries.Find(id);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntry entry)
        {
            // Client side validation
            if (entry.Title.IsNullOrEmpty())
            {
                ModelState.AddModelError("Title", "Title is required");
            }
            if (entry.Content.IsNullOrEmpty())
            {
                ModelState.AddModelError("Content", "Content is required");
            }

            if (ModelState.IsValid)
            {
                // Update database
                _db.DiaryEntries.Update(entry);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Return error message
                return View(entry);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var entry = _db.DiaryEntries.Find(id);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntry entry)
        {
            // Update database
            _db.DiaryEntries.Remove(entry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
