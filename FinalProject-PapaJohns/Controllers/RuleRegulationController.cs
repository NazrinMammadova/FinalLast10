using FinalProject_PapaJohns.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Controllers
{
    public class RuleRegulationController : Controller
    {
        private readonly AppDbContext _context;

        public RuleRegulationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rulesRegulations = _context.RuleRegulations.ToList();
            return View(rulesRegulations);
        }
    }
}
