using FinalProject_PapaJohns.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class SaleController : Controller
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sale = _context.Sales
                .Include(s => s.AppUser)
                .Include(s => s.SaleProducts)
                .ThenInclude(sp => sp.Product)
                .ToList();
            return View(sale);
        }
    }
}
