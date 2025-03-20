using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly MajorService _majorService;

        public MajorController(MajorService majorService)
        {
            _majorService = majorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Major>>> GetMajors()
        {
            return await _majorService.GetMajorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Major>> GetMajorById(int id)
        {
            var major = await _majorService.GetMajorByIdAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            return major;
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Major>> GetMajorWithDetails(int id)
        {
            var major = await _majorService.GetMajorWithDetailsAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            return major;
        }

        [HttpPost]
        public async Task<ActionResult<Major>> PostMajor(Major major)
        {
            var newMajor = await _majorService.AddMajorAsync(major);
            return CreatedAtAction(nameof(GetMajorById), new { id = newMajor.MajorID }, newMajor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMajor(int id, Major major)
        {
            var updated = await _majorService.UpdateMajorAsync(id, major);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajor(int id)
        {
            var deleted = await _majorService.DeleteMajorAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
