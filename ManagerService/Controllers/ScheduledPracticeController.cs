using AutoMapper;
using ManagerService.Data;
using ManagerService.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduledPracticeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ScheduledPracticeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ScheduledPractice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledPracticeDTO>>> GetAllScheduledPractices()
        {
            var scheduledPractices = await _context.ScheduledPractices
                .Include(sp => sp.PracticeType)
                .Include(sp => sp.Specialization)
                .ToListAsync();

            var dtos = _mapper.Map<IEnumerable<ScheduledPracticeDTO>>(scheduledPractices);
            return Ok(dtos);
        }

        // GET: ScheduledPractice/GetById?id=1
        [HttpGet("GetById")]
        public async Task<ActionResult<ScheduledPracticeDTO>> GetScheduledPracticeById(int id)
        {
            var scheduledPractice = await _context.ScheduledPractices
                .Include(sp => sp.PracticeType)
                .Include(sp => sp.Specialization)
                .FirstOrDefaultAsync(sp => sp.IdScheduledPractice == id);

            if (scheduledPractice == null)
                return NotFound($"Scheduled practice with ID {id} not found");

            var dto = _mapper.Map<ScheduledPracticeDTO>(scheduledPractice);
            return Ok(dto);
        }
    }
}