using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
