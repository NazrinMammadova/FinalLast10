using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

    
        public IActionResult SearchProduct(string text)
        {
            var products = _context.Products.Where(p => p.Name.ToLower().Contains(text.ToLower())).OrderByDescending(p => p.Id).Take(4).ToList();
            return PartialView("_searchPartial", products);


        }
        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            var randomProducts = _context.Products
                                 .Where(p => p.Id != id) 
                                 .OrderBy(x => Guid.NewGuid()) 
                                 .Take(8) 
                                 .ToList();

            var pm = new ProductDetailVM
            {
                Product = product,
                RandomProducts = randomProducts
            };



            return View(pm);


        }




    }
}
