using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class CourseService
    {
        private readonly StudentRecordsContext _context;

        public CourseService(StudentRecordsContext context)
        {
            _context = context;
        }

        //  Returns basic Course info
        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }

        //  Returns Course with Department, Faculty, and Term
        public async Task<Course?> GetCourseWithDetailsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Faculty)
                .Include(c => c.Term)
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }

        //  Returns Course with Enrolled Students
        public async Task<Course?> GetCourseWithStudentsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.StudentCourses)
                .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }
    }
}
