using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UMS.DbContexts;
using UMS.Models;

namespace UMS.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        private readonly int _minAge = 16;
        public StudentController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var dataList = await _context.Students.ToListAsync();
            return Ok(dataList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (!ModelState.IsValid) throw new Exception("Sorry! There Some Error in Form Validation!");
            var age = (DateTime.Today - student.Dob.Date).TotalDays / 365;
            if (_minAge > age) throw new Exception("Sorry! Age Must be At least 16+ Years!");

            var isEmailExists = (await _context.Students.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(student.Email.Trim().ToLower()))) != null;
            if (isEmailExists) throw new Exception($"Sorry! This Email [{student.Email}] Already Exists!");

            _context.Students.Add(student);
            var isAdded = await _context.SaveChangesAsync() > 0;

            return Ok(isAdded);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student student)
        {
            if (id != student.Id) return BadRequest();
            if (!ModelState.IsValid) throw new Exception("Sorry! There Some Error in Form Validation!");
            var age = (DateTime.Today - student.Dob.Date).TotalDays / 365;
            if (_minAge > age) throw new Exception("Sorry! Age Must be At least 16+ Years!");

            var isEmailExists = (await _context.Students.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(student.Email.Trim().ToLower()) && c.Id != student.Id)) != null;
            if (isEmailExists) throw new Exception($"Sorry! This Email [{student.Email}] Already Exists!");

            _context.Entry(student).State = EntityState.Modified;
            var isUpdated = await _context.SaveChangesAsync() > 0;
            return Ok(isUpdated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            var isRemoved = await _context.SaveChangesAsync() > 0;
            return Ok(isRemoved);
        }

    }
}
