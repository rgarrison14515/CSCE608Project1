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
    public class StudentCourseController : ControllerBase
    {
        private readonly StudentRecordsContext _context;

        public StudentCourseController(StudentRecordsContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            return await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ToListAsync();
        }

        // GET: api/StudentCourse/{studentId}/{courseId}
        [HttpGet("{studentId}/{courseId}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(string studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);

            if (studentCourse == null)
            {
                return NotFound();
            }

            return studentCourse;
        }

        // POST: api/StudentCourse
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostStudentCourse(StudentCourse studentCourse)
        {
            if (!_context.Students.Any(s => s.StudentID == studentCourse.StudentID))
            {
                return BadRequest("Student does not exist.");
            }

            if (!_context.Courses.Any(c => c.CourseID == studentCourse.CourseID))
            {
                return BadRequest("Course does not exist.");
            }

            _context.StudentCourses.Add(studentCourse);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict("This student is already enrolled in the course.");
            }

            return CreatedAtAction(nameof(GetStudentCourse), new { studentId = studentCourse.StudentID, courseId = studentCourse.CourseID }, studentCourse);
        }

        // DELETE: api/StudentCourse/{studentId}/{courseId}
        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(string studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
