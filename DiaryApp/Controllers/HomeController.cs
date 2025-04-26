using System.Diagnostics;
using Diary.Business.Models;
using Diary.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace DiaryApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDiaryEntryService diaryEntryService, ILogger<HomeController> logger)
        {
            _diaryEntryService = diaryEntryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<DiaryEntryModel> entries = _diaryEntryService.GetAllDiaryEntries().ToList();
            return View(entries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiaryEntryModel entry)
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
                _diaryEntryService.CreateDiaryEntry(entry);
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

            var entry = _diaryEntryService.GetDiaryEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntryModel entry)
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
                _diaryEntryService.UpdateDiaryEntry(entry);
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

            var entry = _diaryEntryService.GetDiaryEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntryModel entry)
        {
            // Update database
            _diaryEntryService.DeleteDiaryEntry(entry.Id);
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
