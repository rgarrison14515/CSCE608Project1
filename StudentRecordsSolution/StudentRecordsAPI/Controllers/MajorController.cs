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
    public class MajorController : ControllerBase
    {
        private readonly StudentRecordsContext _context;

        public MajorController(StudentRecordsContext context)
        {
            _context = context;
        }

        // GET: api/Major
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Major>>> GetMajors()
        {
            return await _context.Majors
                .Include(m => m.Department)
                .Include(m => m.StudentMajors)
                .ThenInclude(sm => sm.Student) // Include students assigned to the major
                .ToListAsync();
        }

        // GET: api/Major/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Major>> GetMajorById(int id)
        {
            var major = await _context.Majors
                .Include(m => m.Department)
                .Include(m => m.StudentMajors)
                .ThenInclude(sm => sm.Student) // Include students assigned to the major
                .FirstOrDefaultAsync(m => m.MajorID == id);

            if (major == null)
            {
                return NotFound();
            }

            return major;
        }

        // POST: api/Major
        [HttpPost]
        public async Task<ActionResult<Major>> PostMajor(Major major)
        {
            _context.Majors.Add(major);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMajorById), new { id = major.MajorID }, major);
        }

        // PUT: api/Major/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMajor(int id, Major major)
        {
            if (id != major.MajorID)
            {
                return BadRequest();
            }

            _context.Entry(major).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Majors.Any(e => e.MajorID == id))
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

        // DELETE: api/Major/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajor(int id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }

            _context.Majors.Remove(major);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
