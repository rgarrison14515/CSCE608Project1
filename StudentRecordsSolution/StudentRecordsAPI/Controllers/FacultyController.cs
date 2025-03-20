using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly StudentRecordsContext _context;

        public FacultyController(StudentRecordsContext context)
        {
            _context = context;
        }

        // GET: api/Faculty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculty()
        {
            return await _context.Faculty
                .Include(f => f.Department)
                .Include(f => f.Courses)
                .ToListAsync();
        }

        // GET: api/Faculty/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFacultyById(int id)
        {
            var faculty = await _context.Faculty
                .Include(f => f.Department)
                .Include(f => f.Courses)
                .FirstOrDefaultAsync(f => f.FacultyID == id);

            if (faculty == null)
            {
                return NotFound();
            }

            return faculty;
        }

        // POST: api/Faculty
        [HttpPost]
        public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
        {
            _context.Faculty.Add(faculty);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFacultyById), new { id = faculty.FacultyID }, faculty);
        }

        // PUT: api/Faculty/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
        {
            if (id != faculty.FacultyID)
            {
                return BadRequest();
            }

            _context.Entry(faculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Faculty.Any(e => e.FacultyID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Faculty/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            var faculty = await _context.Faculty.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            _context.Faculty.Remove(faculty);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
