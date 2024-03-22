using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UMS.DbContexts;
using UMS.Models;

namespace UMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly ProjectDbContext _context;
        private readonly int _minAge = 16;

        public StudentController(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            SetUserId();
            var dataList = await _context.Students.Include(s => s.Subject).ToListAsync();
            return View(dataList);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            SetUserId();

            if (id == null) return NotFound();
            var student = await _context.Students.Include(s => s.Subject).FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        public IActionResult Create()
        {
            SetUserId();
            GetSubjectsSli();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            SetUserId();
            GetSubjectsSli(student);
            if (!ModelState.IsValid) return View(student);
            var age = (DateTime.Today - student.Dob.Date).TotalDays / 365;
            if (_minAge > age)
            {
                ViewBag.ErrorMsg = "Sorry! Age Must be At least 16+ Years!";
                return View(student);

            }
            var isEmailExists = (await _context.Students.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(student.Email.Trim().ToLower()))) != null;
            if (isEmailExists)
            {
                ViewBag.ErrorMsg = $"Sorry! This Email [{student.Email}] Already Exists!";
                return View(student);
            }


            student.Id = Guid.NewGuid();
            _context.Add(student);
            var isAdded = await _context.SaveChangesAsync() > 0;
            if (isAdded && HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (isAdded)
            {
                return RedirectToAction(nameof(Details), new { id = student.Id });
            }

            return View(student);

        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            SetUserId();
            if (id == null) return NotFound();
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            GetSubjectsSli(student);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Student student)
        {
            SetUserId();
            if (id != student.Id) return NotFound();
            var age = (DateTime.Today - student.Dob.Date).TotalDays / 365;
            if (_minAge > age)
            {
                ViewBag.ErrorMsg = "Sorry! Age Must be At least 16+ Years!";
                return View(student);

            }

            var isEmailExists = (await _context.Students.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(student.Email.Trim().ToLower()) && c.Id != student.Id)) != null;
            if (isEmailExists)
            {
                ViewBag.ErrorMsg = $"Sorry! This Email [{student.Email}] Already Exists!";
                return View(student);
            }


            GetSubjectsSli(student);
            if (!ModelState.IsValid) return View(student);
            _context.Update(student);
            var isUpdated = await _context.SaveChangesAsync() > 0;
            if (isUpdated) return RedirectToAction(nameof(Index));

            return View(student);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            SetUserId();
            if (id == null) return NotFound();
            var student = await _context.Students.Include(s => s.Subject).FirstOrDefaultAsync(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            SetUserId();
            var student = await _context.Students.FindAsync(id);
            if (student != null) _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private void GetSubjectsSli(Student student = null)
        {
            student ??= new Student();
            ViewBag.SubjectId = new SelectList(_context.Subjects, "Id", "Name", student.SubjectId);
        }

        private void SetUserId() => ViewBag.UserId = HttpContext.Session.GetString("UserId");
    }
}
