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
    public class StudentMajorController : ControllerBase
    {
        private readonly StudentRecordsContext _context;

        public StudentMajorController(StudentRecordsContext context)
        {
            _context = context;
        }

        // GET: api/StudentMajor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentMajor>>> GetStudentMajors()
        {
            return await _context.StudentMajors
                .Include(sm => sm.Student)
                .Include(sm => sm.Major)
                .ToListAsync();
        }

        // GET: api/StudentMajor/{studentId}/{majorId}
        [HttpGet("{studentId}/{majorId}")]
        public async Task<ActionResult<StudentMajor>> GetStudentMajor(string studentId, int majorId)
        {
            var studentMajor = await _context.StudentMajors
                .Include(sm => sm.Student)
                .Include(sm => sm.Major)
                .FirstOrDefaultAsync(sm => sm.StudentID == studentId && sm.MajorID == majorId);

            if (studentMajor == null)
            {
                return NotFound();
            }

            return studentMajor;
        }

        // POST: api/StudentMajor
        [HttpPost]
        public async Task<ActionResult<StudentMajor>> PostStudentMajor(StudentMajor studentMajor)
        {
            if (!_context.Students.Any(s => s.StudentID == studentMajor.StudentID))
            {
                return BadRequest("Student does not exist.");
            }

            if (!_context.Majors.Any(m => m.MajorID == studentMajor.MajorID))
            {
                return BadRequest("Major does not exist.");
            }

            _context.StudentMajors.Add(studentMajor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("This student is already assigned to this major.");
            }

            return CreatedAtAction(nameof(GetStudentMajor), new { studentId = studentMajor.StudentID, majorId = studentMajor.MajorID }, studentMajor);
        }

        // DELETE: api/StudentMajor/{studentId}/{majorId}
        [HttpDelete("{studentId}/{majorId}")]
        public async Task<IActionResult> DeleteStudentMajor(string studentId, int majorId)
        {
            var studentMajor = await _context.StudentMajors.FindAsync(studentId, majorId);
            if (studentMajor == null)
            {
                return NotFound();
            }

            _context.StudentMajors.Remove(studentMajor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
