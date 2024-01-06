using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.HomeSliders = _context.HomeSliders.ToList();
            homeVM.PapaServices = _context.PapaServices.ToList();
            homeVM.PizzaPictures = _context.pizzaPictures.ToList();
            homeVM.MenuPicture = _context.MenuPictures.FirstOrDefault();
            homeVM.SloganPapaJohns = _context.SloganPapaJohns.FirstOrDefault();
            return View(homeVM);

        }
    }
}
