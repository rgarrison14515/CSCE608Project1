using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordsAPI.Models.DTOs; // Import DTOs

namespace StudentRecordsAPI.Services
{
    public class StudentService
    {
        private readonly StudentRecordsContext _context;

        public StudentService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(string studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentID == studentId);
        }

        public async Task<Student?> GetStudentWithDetailsAsync(string studentId)
        {
            return await _context.Students
                .Include(s => s.Major)
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateStudentAsync(string studentId, Student student)
        {
            if (studentId != student.StudentID)
            {
                return false;
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Students.AnyAsync(s => s.StudentID == studentId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetStudentsByMajorAsync(int majorId)
        {
            return await _context.Students
                .Where(s => s.MajorID == majorId)
                .ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByCourseAsync(int courseId)
        {
            return await _context.StudentCourses
                .Where(sc => sc.CourseID == courseId)
                .Select(sc => sc.Student)
                .ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByCourseNameAsync(string courseName)
        {
            var course = await _context.Courses
                .Where(c => c.Title == courseName)
                .Select(c => c.CourseID)
                .FirstOrDefaultAsync();

            if (course == 0) // If no course found
            {
                return new List<Student>();
            }

            return await _context.StudentCourses
                .Where(sc => sc.CourseID == course)
                .Select(sc => new Student
                {
                    StudentID = sc.Student.StudentID,
                    Name = sc.Student.Name,
                    Email = sc.Student.Email,
                    MajorID = sc.Student.MajorID
                })
                .ToListAsync();
        }

        public async Task<List<Student>> GetStudentsByMajorNameAsync(string majorName)
        {
            var major = await _context.Majors
                .Where(m => m.Name == majorName)
                .Select(m => m.MajorID)
                .FirstOrDefaultAsync();

            if (major == 0) // If no major found
            {
                return new List<Student>();
            }

            return await _context.Students
                .Where(s => s.MajorID == major)
                .Select(s => new Student
                {
                    StudentID = s.StudentID,
                    Name = s.Name,
                    Email = s.Email,
                    MajorID = s.MajorID
                })
                .ToListAsync();
        }

        public async Task<List<StudentWithMajorsDto>> GetStudentsWithMultipleMajorsAsync()
        {
            return await _context.StudentMajors
                .Include(sm => sm.Student)  // Ensure Student is included
                .Include(sm => sm.Major)    // Ensure Major is included
                .GroupBy(sm => sm.StudentID)
                .Where(g => g.Count() >= 2) // Only students with 2+ majors
                .Select(g => new StudentWithMajorsDto
                {
                    StudentID = g.Key,
                    Name = g.First().Student != null ? g.First().Student.Name : "Unknown",
                    Email = g.First().Student != null ? g.First().Student.Email : "Unknown",
                    Majors = g.Where(sm => sm.Major != null).Select(sm => sm.Major.Name).ToList() // Avoid null reference
                })
                .ToListAsync();
        }




    }
}
