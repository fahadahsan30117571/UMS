using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UMS.DbContexts;
using UMS.Models;

namespace UMS.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ProjectDbContext _context;

        public SubjectController(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            SetUserId();
            var dataList = await _context.Subjects.ToListAsync();
            return View(dataList);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null) return NotFound();
            SetUserId();

            return View(subject);
        }

        public IActionResult Create()
        {
            SetUserId();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            SetUserId();
            if (!ModelState.IsValid) throw new Exception("Sorry! There Some Error in Form Validation!");
            var isExists = (await _context.Subjects.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(subject.Name.Trim().ToLower()))) != null;
            if (isExists)
            {
                ViewBag.ErrorMsg = $"Sorry! This Subject [{subject.Name}] Already Exists!";
                return View(subject);
            }

            subject.Id = Guid.NewGuid();
            _context.Add(subject);
            var isAdded = await _context.SaveChangesAsync() > 0;
            if (isAdded) return RedirectToAction(nameof(Index));
            return View(subject);

        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            SetUserId();
            if (id == null) return NotFound();
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Subject subject)
        {
            SetUserId();
            if (id != subject.Id) return NotFound();

            var isExists = (await _context.Subjects.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(subject.Name.Trim().ToLower()) && c.Id != subject.Id)) != null;
            if (isExists)
            {
                ViewBag.ErrorMsg = $"Sorry! This Subject [{subject.Name}] Already Exists!";
                return View(subject);
            }

            if (ModelState.IsValid)
            {
                _context.Update(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            SetUserId();
            if (id == null) return NotFound();
            var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            SetUserId();
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(Guid id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }

        private void SetUserId() => ViewBag.UserId = HttpContext.Session.GetString("UserId");

    }
}
