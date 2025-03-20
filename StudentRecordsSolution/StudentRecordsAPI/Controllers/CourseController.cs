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

        //  Returns Course basic details (No related data)
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

        //  Returns Course *WITH* Department, Faculty, and Term
        [HttpGet("{id}/details")]
        public async Task<ActionResult<Course>> GetCourseWithDetails(int id)
        {
            var course = await _courseService.GetCourseWithDetailsAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }

        //  Returns Course *WITH* Enrolled Students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<Course>> GetCourseWithStudents(int id)
        {
            var course = await _courseService.GetCourseWithStudentsAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }
    }
}
