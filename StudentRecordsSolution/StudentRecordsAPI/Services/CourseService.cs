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

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Faculty)
                .Include(c => c.Term)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Faculty)
                .Include(c => c.Term)
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(int id, Course course)
        {
            if (id != course.CourseID)
            {
                return false;
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Courses.AnyAsync(e => e.CourseID == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetCoursesByFacultyAsync(int facultyId)
        {
            return await _context.Courses
                .Where(c => c.FacultyID == facultyId)
                .Include(c => c.Department)
                .Include(c => c.Term)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByDepartmentAsync(int departmentId)
        {
            return await _context.Courses
                .Where(c => c.DepartmentID == departmentId)
                .Include(c => c.Faculty)
                .Include(c => c.Term)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByTermAsync(int termId)
        {
            return await _context.Courses
                .Where(c => c.TermID == termId)
                .Include(c => c.Department)
                .Include(c => c.Faculty)
                .ToListAsync();
        }
    }
}
