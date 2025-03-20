using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly FacultyService _facultyService;

        public FacultyController(FacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculty()
        {
            return await _facultyService.GetFacultyAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetFacultyById(int id)
        {
            var faculty = await _facultyService.GetFacultyByIdAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return faculty;
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Faculty>> GetFacultyWithDetails(int id)
        {
            var faculty = await _facultyService.GetFacultyWithDetailsAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return faculty;
        }

        [HttpPost]
        public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
        {
            var newFaculty = await _facultyService.AddFacultyAsync(faculty);
            return CreatedAtAction(nameof(GetFacultyById), new { id = newFaculty.FacultyID }, newFaculty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
        {
            var updated = await _facultyService.UpdateFacultyAsync(id, faculty);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(int id)
        {
            var deleted = await _facultyService.DeleteFacultyAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("department/name/{departmentName}")]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFacultyByDepartmentName(string departmentName)
        {
            var faculty = await _facultyService.GetFacultyByDepartmentNameAsync(departmentName);

            if (faculty == null || faculty.Count == 0)
            {
                return NotFound($"No faculty members found for department: {departmentName}");
            }

            return faculty;
        }

    }
}
