using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class StudentMajorService
    {
        private readonly StudentRecordsContext _context;

        public StudentMajorService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<StudentMajor>> GetStudentMajorsAsync()
        {
            return await _context.StudentMajors
                .Include(sm => sm.Student)
                .Include(sm => sm.Major)
                .ToListAsync();
        }

        public async Task<StudentMajor?> GetStudentMajorAsync(string studentId, int majorId)
        {
            return await _context.StudentMajors
                .Include(sm => sm.Student)
                .Include(sm => sm.Major)
                .FirstOrDefaultAsync(sm => sm.StudentID == studentId && sm.MajorID == majorId);
        }

        public async Task<StudentMajor?> AddStudentMajorAsync(StudentMajor studentMajor)
        {
            if (!_context.Students.Any(s => s.StudentID == studentMajor.StudentID))
            {
                return null; // Student does not exist
            }

            if (!_context.Majors.Any(m => m.MajorID == studentMajor.MajorID))
            {
                return null; // Major does not exist
            }

            _context.StudentMajors.Add(studentMajor);

            try
            {
                await _context.SaveChangesAsync();
                return studentMajor;
            }
            catch (DbUpdateException)
            {
                return null; // Conflict - student is already assigned to this major
            }
        }

        public async Task<bool> DeleteStudentMajorAsync(string studentId, int majorId)
        {
            var studentMajor = await _context.StudentMajors.FindAsync(studentId, majorId);
            if (studentMajor == null)
            {
                return false;
            }

            _context.StudentMajors.Remove(studentMajor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
