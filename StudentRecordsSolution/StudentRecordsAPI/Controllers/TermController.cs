using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly TermService _termService;

        public TermController(TermService termService)
        {
            _termService = termService;
        }

        // GET: api/Term
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Term>>> GetTerms()
        {
            return await _termService.GetTermsAsync();
        }

        // GET: api/Term/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Term>> GetTermById(int id)
        {
            var term = await _termService.GetTermByIdAsync(id);
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
            var newTerm = await _termService.AddTermAsync(term);
            return CreatedAtAction(nameof(GetTermById), new { id = newTerm.TermID }, newTerm);
        }

        // PUT: api/Term/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerm(int id, Term term)
        {
            var updated = await _termService.UpdateTermAsync(id, term);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Term/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerm(int id)
        {
            var deleted = await _termService.DeleteTermAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
