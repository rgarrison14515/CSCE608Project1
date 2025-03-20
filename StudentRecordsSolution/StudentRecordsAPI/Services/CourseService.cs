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

        public async Task<List<Course>> GetCoursesByFacultyNameAsync(string facultyName)
        {
            var faculty = await _context.Faculty
                .Where(f => f.Name == facultyName)
                .Select(f => f.FacultyID)
                .FirstOrDefaultAsync();

            if (faculty == 0) // If no faculty found
            {
                return new List<Course>();
            }

            return await _context.Courses
                .Where(c => c.FacultyID == faculty)
                .Select(c => new Course
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentID = c.DepartmentID,
                    FacultyID = c.FacultyID,
                    TermID = c.TermID
                })
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByTermNameAsync(string termName)
        {
            var term = await _context.Terms
                .Where(t => t.TermCode == termName)
                .Select(t => t.TermID)
                .FirstOrDefaultAsync();

            if (term == 0) // If no term found
            {
                return new List<Course>();
            }

            return await _context.Courses
                .Where(c => c.TermID == term)
                .Select(c => new Course
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Credits = c.Credits,
                    DepartmentID = c.DepartmentID,
                    FacultyID = c.FacultyID,
                    TermID = c.TermID
                })
                .ToListAsync();
        }

    }
}
