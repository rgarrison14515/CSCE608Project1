using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Services
{
    public class TermService
    {
        private readonly StudentRecordsContext _context;

        public TermService(StudentRecordsContext context)
        {
            _context = context;
        }

        public async Task<List<Term>> GetTermsAsync()
        {
            return await _context.Terms
                .Include(t => t.Courses) // Include related courses
                .ToListAsync();
        }

        public async Task<Term?> GetTermByIdAsync(int id)
        {
            return await _context.Terms
                .FirstOrDefaultAsync(t => t.TermID == id); //  Only the term, no courses
        }

        public async Task<Term?> GetTermWithCoursesByIdAsync(int id)
        {
            return await _context.Terms
                .Include(t => t.Courses) //  Now we include courses
                .FirstOrDefaultAsync(t => t.TermID == id);
        }


        public async Task<Term> AddTermAsync(Term term)
        {
            _context.Terms.Add(term);
            await _context.SaveChangesAsync();
            return term;
        }

        public async Task<bool> UpdateTermAsync(int id, Term term)
        {
            if (id != term.TermID)
            {
                return false;
            }

            _context.Entry(term).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Terms.AnyAsync(e => e.TermID == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteTermAsync(int id)
        {
            var term = await _context.Terms.FindAsync(id);
            if (term == null)
            {
                return false;
            }

            _context.Terms.Remove(term);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
