﻿using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMajorController : ControllerBase
    {
        private readonly StudentMajorService _studentMajorService;

        public StudentMajorController(StudentMajorService studentMajorService)
        {
            _studentMajorService = studentMajorService;
        }

        // GET: api/StudentMajor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentMajor>>> GetStudentMajors()
        {
            return await _studentMajorService.GetStudentMajorsAsync();
        }

        // GET: api/StudentMajor/{studentId}/{majorId}
        [HttpGet("{studentId}/{majorId}")]
        public async Task<ActionResult<StudentMajor>> GetStudentMajor(string studentId, int majorId)
        {
            var studentMajor = await _studentMajorService.GetStudentMajorAsync(studentId, majorId);
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
            var addedStudentMajor = await _studentMajorService.AddStudentMajorAsync(studentMajor);
            if (addedStudentMajor == null)
            {
                return Conflict("Student or Major does not exist, or the student is already assigned.");
            }

            return CreatedAtAction(nameof(GetStudentMajor), new { studentId = studentMajor.StudentID, majorId = studentMajor.MajorID }, studentMajor);
        }

        // DELETE: api/StudentMajor/{studentId}/{majorId}
        [HttpDelete("{studentId}/{majorId}")]
        public async Task<IActionResult> DeleteStudentMajor(string studentId, int majorId)
        {
            var deleted = await _studentMajorService.DeleteStudentMajorAsync(studentId, majorId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
