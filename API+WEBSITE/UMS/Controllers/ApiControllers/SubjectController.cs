using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UMS.DbContexts;
using UMS.Models;

namespace UMS.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public SubjectController(ProjectDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            var dataLis = await _context.Subjects.ToListAsync();
            return Ok(dataLis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return Ok(subject);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            if (!ModelState.IsValid) throw new Exception("Sorry! There Some Error in Form Validation!");

            var isExists = (await _context.Subjects.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(subject.Name.Trim().ToLower()))) != null;
            if (isExists) throw new Exception($"Sorry! This Subject [{subject.Name}] Already Exists!");

            _context.Subjects.Add(subject);
            var isAdded = await _context.SaveChangesAsync() > 0;
            return Ok(isAdded);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(Guid id, Subject subject)
        {
            if (id != subject.Id) return BadRequest();
            if (!ModelState.IsValid) throw new Exception("Sorry! There Some Error in Form Validation!");
            var isExists = (await _context.Subjects.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(subject.Name.Trim().ToLower()) && c.Id != subject.Id)) != null;
            if (isExists) throw new Exception($"Sorry! This Subject [{subject.Name}] Already Exists!");

            _context.Entry(subject).State = EntityState.Modified;
            var isUpdated = await _context.SaveChangesAsync() > 0;
            return Ok(isUpdated);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            _context.Subjects.Remove(subject);
            var isDeleted = await _context.SaveChangesAsync() > 0;
            return Ok(isDeleted);
        }

    }
}
