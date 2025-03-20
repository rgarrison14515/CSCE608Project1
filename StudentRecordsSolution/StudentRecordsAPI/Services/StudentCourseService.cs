using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class StudentCourseService
    {
        private readonly StudentRecordsContext _context;

        public StudentCourseService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<StudentCourse>> GetStudentCoursesAsync()
        {
            return await _context.StudentCourses.ToListAsync();
        }

        public async Task<StudentCourse?> GetStudentCourseAsync(string studentId, int courseId)
        {
            return await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
        }

        public async Task<StudentCourse?> GetStudentCourseWithDetailsAsync(string studentId, int courseId)
        {
            return await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
        }

        public async Task<StudentCourse?> AddStudentCourseAsync(StudentCourse studentCourse)
        {
            if (!_context.Students.Any(s => s.StudentID == studentCourse.StudentID))
            {
                return null;
            }

            if (!_context.Courses.Any(c => c.CourseID == studentCourse.CourseID))
            {
                return null;
            }

            _context.StudentCourses.Add(studentCourse);

            try
            {
                await _context.SaveChangesAsync();
                return studentCourse;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteStudentCourseAsync(string studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return false;
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
