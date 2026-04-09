using ManagerService.Data;
using ManagerService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ManagerService.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PracticeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracticeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetById")]
        public IActionResult GetPracticeTypeById([FromQuery]int id)
        {
            var practiceType = _context.PracticeTypes
            .Where(pt => pt.IdPracticeType == id)
            .Select(pt => new PracticeTypeDTO
            {
                IdPracticeType = pt.IdPracticeType,
                Name = pt.Name,
                Description = pt.Description
            })
            .FirstOrDefault();

            if (practiceType == null)
            {
                return NotFound("нет такого типа практики");
            }
            return Ok(practiceType);
        }

        [HttpGet]
        public ActionResult<List<PracticeTypeDTO>> GetPracticeTypes()
        {
            var practiceTypes = _context.PracticeTypes
            .Select(pt => new PracticeTypeDTO
            {
                IdPracticeType = pt.IdPracticeType,
                Name = pt.Name,
                Description = pt.Description
            })
            .ToList(); //как-то обработать ситуацию, когда сервер не отвечает

            if (practiceTypes.Count == 0)
            {
                return NotFound();
            }
            return Ok(practiceTypes);
        }
    }
}