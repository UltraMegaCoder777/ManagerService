using ManagerService.Data;
using ManagerService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagerService.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetById")]
        public IActionResult GetDepartmentById([FromQuery] int id)
        {
            var department = _context.Departments
            .Where(pt => pt.IdDepartment == id)
            .Select(pt => new DepartmentDTO
            {
                IdDepartment = pt.IdDepartment,
                Name = pt.Name
            })
            .FirstOrDefault();

            if (department == null)
            {
                return NotFound("нет такого подразделения");
            }
            return Ok(department);
        }

        [HttpGet]
        public ActionResult<List<DepartmentDTO>> GetDepartments()
        {
            var department = _context.Departments
            .Select(pt => new DepartmentDTO
            {
                IdDepartment = pt.IdDepartment,
                Name = pt.Name
            })
            .ToList();

            if (department.Count == 0)
            {
                return NotFound();
            }
            return Ok(department);
        }
    }
}
