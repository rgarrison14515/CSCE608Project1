using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _courseService.GetCoursesAsync();
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            var newCourse = await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = newCourse.CourseID }, newCourse);
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            var updated = await _courseService.UpdateCourseAsync(id, course);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/Course/faculty/{facultyId}
        [HttpGet("faculty/{facultyId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByFaculty(int facultyId)
        {
            return await _courseService.GetCoursesByFacultyAsync(facultyId);
        }

        // GET: api/Course/department/{departmentId}
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByDepartment(int departmentId)
        {
            return await _courseService.GetCoursesByDepartmentAsync(departmentId);
        }

        // GET: api/Course/term/{termId}
        [HttpGet("term/{termId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTerm(int termId)
        {
            return await _courseService.GetCoursesByTermAsync(termId);
        }
    }
}
