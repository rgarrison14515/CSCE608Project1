﻿using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentRecordsAPI.Models.DTOs; // Import DTOs

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentService.GetStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Student>> GetStudentWithDetails(string id)
        {
            var student = await _studentService.GetStudentWithDetailsAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var newStudent = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.StudentID }, newStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string id, Student student)
        {
            var updated = await _studentService.UpdateStudentAsync(id, student);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("major/{majorId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByMajor(int majorId)
        {
            return await _studentService.GetStudentsByMajorAsync(majorId);
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourse(int courseId)
        {
            return await _studentService.GetStudentsByCourseAsync(courseId);
        }

        [HttpGet("course/name/{courseName}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourseName(string courseName)
        {
            var students = await _studentService.GetStudentsByCourseNameAsync(courseName);

            if (students == null || students.Count == 0)
            {
                return NotFound($"No students found for course: {courseName}");
            }

            return students;
        }

        [HttpGet("major/name/{majorName}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByMajorName(string majorName)
        {
            var students = await _studentService.GetStudentsByMajorNameAsync(majorName);

            if (students == null || students.Count == 0)
            {
                return NotFound($"No students found for major: {majorName}");
            }

            return students;
        }

        [HttpGet("multiple-majors")]
        public async Task<ActionResult<IEnumerable<StudentWithMajorsDto>>> GetStudentsWithMultipleMajors()
        {
            var students = await _studentService.GetStudentsWithMultipleMajorsAsync();

            if (students == null || students.Count == 0)
            {
                return NotFound("No students found with multiple majors.");
            }

            return students;
        }


    }
}
