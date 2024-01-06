using FinalProject_PapaJohns.Extension;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.HomeSliderVM;
using FinalProject_PapaJohns.ViewModels.Admin.PizzaServicesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class PizzaServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PizzaServicesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var services = _context.PapaServices.ToList();
            return View(services);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existPizzaService = _context.PapaServices.FirstOrDefault(x => x.Id == id);
            if (existPizzaService == null) return NotFound();
            string path = Path.Combine(webHostEnvironment.WebRootPath, "img", existPizzaService.ImageUrl);
            if (System.IO.File.Exists(path))

            {
                System.IO.File.Delete(path);

            }
            _context.PapaServices.Remove(existPizzaService);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreatePizzaServicesVM CreatePizzaServicesVM)
        {


            PapaService PapaService = new();


            PapaService.ImageUrl = CreatePizzaServicesVM.Photo;
            PapaService.CreatedAt = DateTime.Now;
            PapaService.AddedBy = CreatePizzaServicesVM.AddedBy;
            PapaService.Desc = CreatePizzaServicesVM.Desciption;
            PapaService.Name = CreatePizzaServicesVM.Title;

            _context.PapaServices.Add(PapaService);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }


        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existHomeSlider = _context.HomeSliders.FirstOrDefault(s => s.Id == id);
            if (existHomeSlider == null) return NotFound();

            return View(new UpdatePizzaServicesVM
            {
                Photo = existHomeSlider.ImageUrl

            });

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdatePizzaServicesVM UpdatePizzaServicesVM)
        {
            var existPizzaServices = _context.PapaServices.FirstOrDefault(s => s.Id == id);
            if (existPizzaServices == null) return NotFound();

            if (UpdatePizzaServicesVM.Photo != null)
            {
               
                
                existPizzaServices.ImageUrl = UpdatePizzaServicesVM.Photo;
                existPizzaServices.AddedBy = UpdatePizzaServicesVM.AddedBy;
                existPizzaServices.UpdatedAt = DateTime.Now;
                existPizzaServices.Desc = UpdatePizzaServicesVM.Desciption;
                existPizzaServices.Name = UpdatePizzaServicesVM.Title;



                _context.SaveChanges();

            }

            return RedirectToAction("Index");


        }


    }
}
