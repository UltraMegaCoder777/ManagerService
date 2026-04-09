using ManagerService.Data;
using ManagerService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagerService.Controllers.v1
{
    [ApiController]
    [Asp.Versioning.ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecializationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetById")]
        public IActionResult GetSpecializationById([FromQuery]int id)
        {
            var Specialization = _context.Specializations
            .Where(pt => pt.IdSpecialization == id)
            .Select(pt => new SpecializationDTO
            {
                IdSpecialization = pt.IdSpecialization,
                Name = pt.Name,
                Description = pt.Description
            })
            .FirstOrDefault();

            if (Specialization == null)
            {
                return NotFound("нет такой специальности");
            }
            return Ok(Specialization);
        }

        [HttpGet]
        public ActionResult<List<SpecializationDTO>> GetSpecializations()
        {
            var specialization = _context.Specializations
            .Select(pt => new SpecializationDTO
            {
                IdSpecialization = pt.IdSpecialization,
                Name = pt.Name,
                Description = pt.Description
            })
            .ToList();

            if (specialization.Count == 0)
            {
                return NotFound();
            }
            return Ok(specialization);
        }
    }
}
