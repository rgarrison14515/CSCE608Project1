using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Data;
using StudentRecordsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly StudentRecordsContext _context;

        public TermController(StudentRecordsContext context)
        {
            _context = context;
        }

        // GET: api/Term
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Term>>> GetTerms()
        {
            return await _context.Terms
                .Include(t => t.Courses) // Include related courses
                .ToListAsync();
        }

        // GET: api/Term/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Term>> GetTermById(int id)
        {
            var term = await _context.Terms
                .Include(t => t.Courses) // Include related courses
                .FirstOrDefaultAsync(t => t.TermID == id);

            if (term == null)
            {
                return NotFound();
            }

            return term;
        }

        // POST: api/Term
        [HttpPost]
        public async Task<ActionResult<Term>> PostTerm(Term term)
        {
            _context.Terms.Add(term);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTermById), new { id = term.TermID }, term);
        }

        // PUT: api/Term/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerm(int id, Term term)
        {
            if (id != term.TermID)
            {
                return BadRequest();
            }

            _context.Entry(term).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Terms.Any(e => e.TermID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Term/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerm(int id)
        {
            var term = await _context.Terms.FindAsync(id);
            if (term == null)
            {
                return NotFound();
            }

            _context.Terms.Remove(term);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
