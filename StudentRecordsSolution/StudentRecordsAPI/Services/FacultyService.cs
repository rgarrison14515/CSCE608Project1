using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class FacultyService
    {
        private readonly StudentRecordsContext _context;

        public FacultyService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<Faculty>> GetFacultyAsync()
        {
            return await _context.Faculty.ToListAsync();
        }

        public async Task<Faculty?> GetFacultyByIdAsync(int id)
        {
            return await _context.Faculty.FirstOrDefaultAsync(f => f.FacultyID == id);
        }

        public async Task<Faculty?> GetFacultyWithDetailsAsync(int id)
        {
            return await _context.Faculty
                .Include(f => f.Department)
                .Include(f => f.Courses)
                .FirstOrDefaultAsync(f => f.FacultyID == id);
        }

        public async Task<Faculty> AddFacultyAsync(Faculty faculty)
        {
            _context.Faculty.Add(faculty);
            await _context.SaveChangesAsync();
            return faculty;
        }

        public async Task<bool> UpdateFacultyAsync(int id, Faculty faculty)
        {
            if (id != faculty.FacultyID)
            {
                return false;
            }

            _context.Entry(faculty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Faculty.AnyAsync(e => e.FacultyID == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteFacultyAsync(int id)
        {
            var faculty = await _context.Faculty.FindAsync(id);
            if (faculty == null)
            {
                return false;
            }

            _context.Faculty.Remove(faculty);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Faculty>> GetFacultyByDepartmentNameAsync(string departmentName)
        {
            var department = await _context.Departments
                .Where(d => d.Name == departmentName)
                .Select(d => d.DepartmentID)
                .FirstOrDefaultAsync();

            if (department == 0) // If no department found
            {
                return new List<Faculty>();
            }

            return await _context.Faculty
                .Where(f => f.DepartmentID == department)
                .Select(f => new Faculty
                {
                    FacultyID = f.FacultyID,
                    Name = f.Name,
                    Email = f.Email,
                    DepartmentID = f.DepartmentID
                })
                .ToListAsync();
        }

    }
}
