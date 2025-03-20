using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly StudentCourseService _studentCourseService;

        public StudentCourseController(StudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        // GET: api/StudentCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            return await _studentCourseService.GetStudentCoursesAsync();
        }

        // GET: api/StudentCourse/{studentId}/{courseId}
        [HttpGet("{studentId}/{courseId}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(string studentId, int courseId)
        {
            var studentCourse = await _studentCourseService.GetStudentCourseAsync(studentId, courseId);
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
            var addedStudentCourse = await _studentCourseService.AddStudentCourseAsync(studentCourse);
            if (addedStudentCourse == null)
            {
                return Conflict("Student or Course does not exist, or the student is already enrolled.");
            }

            return CreatedAtAction(nameof(GetStudentCourse), new { studentId = studentCourse.StudentID, courseId = studentCourse.CourseID }, studentCourse);
        }

        // DELETE: api/StudentCourse/{studentId}/{courseId}
        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteStudentCourse(string studentId, int courseId)
        {
            var deleted = await _studentCourseService.DeleteStudentCourseAsync(studentId, courseId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
