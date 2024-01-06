using FinalProject_PapaJohns.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Controllers
{
    public class AboutPapaJhonsController : Controller
    {
        private readonly AppDbContext _context;

        public AboutPapaJhonsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var KeyAndValues = _context.AboutPapaJohns.ToDictionary(A=>A.Key, A => A.Value);
            return View(KeyAndValues);
        }
    }
}
