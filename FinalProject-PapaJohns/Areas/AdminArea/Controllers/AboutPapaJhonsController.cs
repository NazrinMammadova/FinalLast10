using FinalProject_PapaJohns.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class AboutPapaJhonsController : Controller
    {
        private readonly AppDbContext _context;

        public AboutPapaJhonsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var AboutPapaJhonsKeyAndValues = _context.AboutPapaJohns.ToDictionary(A=>A.Key, A => A.Value);
            return View(AboutPapaJhonsKeyAndValues);
        }
    }
}
