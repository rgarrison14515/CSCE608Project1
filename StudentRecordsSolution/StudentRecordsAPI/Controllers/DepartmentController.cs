using Microsoft.AspNetCore.Mvc;
using StudentRecordsAPI.Models;
using StudentRecordsAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _departmentService.GetDepartmentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Department>> GetDepartmentWithDetails(int id)
        {
            var department = await _departmentService.GetDepartmentWithDetailsAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var newDepartment = await _departmentService.AddDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = newDepartment.DepartmentID }, newDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            var updated = await _departmentService.UpdateDepartmentAsync(id, department);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _departmentService.DeleteDepartmentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
