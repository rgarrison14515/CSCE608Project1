using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class MajorService
    {
        private readonly StudentRecordsContext _context;

        public MajorService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<Major>> GetMajorsAsync()
        {
            return await _context.Majors
                .Include(m => m.Department)
                .Include(m => m.StudentMajors)
                .ThenInclude(sm => sm.Student) // Include students assigned to the major
                .ToListAsync();
        }

        public async Task<Major?> GetMajorByIdAsync(int id)
        {
            return await _context.Majors
                .Include(m => m.Department)
                .Include(m => m.StudentMajors)
                .ThenInclude(sm => sm.Student) // Include students assigned to the major
                .FirstOrDefaultAsync(m => m.MajorID == id);
        }

        public async Task<Major> AddMajorAsync(Major major)
        {
            _context.Majors.Add(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<bool> UpdateMajorAsync(int id, Major major)
        {
            if (id != major.MajorID)
            {
                return false;
            }

            _context.Entry(major).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Majors.AnyAsync(e => e.MajorID == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteMajorAsync(int id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return false;
            }

            _context.Majors.Remove(major);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
