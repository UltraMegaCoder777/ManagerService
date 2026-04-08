using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagerService.Data;
using ManagerService.Models;

namespace ManagerService.Controllers
{
    public class PracticeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracticeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PracticeTypes
        public async Task<IActionResult> Index()
        {
            // Получаем все записи из таблицы PracticeTypes
            var practiceTypes = await _context.PracticeTypes.ToListAsync();

            // Передаем их в представление
            return View(practiceTypes);
        }
    }
}