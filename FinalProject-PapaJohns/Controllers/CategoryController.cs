using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {

            CategoryVM catVM = new();

            if (id == null)
            {
                return NotFound();
            }
            catVM.Category = _context.Categories
                  .Include(c => c.Products)
                  .FirstOrDefault(c => c.Id == id);






            return View(catVM);

        }
    }
}