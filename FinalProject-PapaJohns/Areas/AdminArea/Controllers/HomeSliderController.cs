using FinalProject_PapaJohns.Extension;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.HomeSliderVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]


    public class HomeSliderController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeSliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var HomeSliders = _context.HomeSliders.ToList();
            return View(HomeSliders);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existHomeSlider = _context.HomeSliders.FirstOrDefault(x => x.Id == id);
            if (existHomeSlider == null) return NotFound();
            string path = Path.Combine(webHostEnvironment.WebRootPath, "img", existHomeSlider.ImageUrl);
            if (System.IO.File.Exists(path))

            {
                System.IO.File.Delete(path);

            }
            _context.HomeSliders.Remove(existHomeSlider);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateHomeSliderVM  createHomeSliderVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (createHomeSliderVM.Photo.IsImage())
            {

                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (createHomeSliderVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "sekil boyukdur");
                return View();
            }
            HomeSlider HomeSlider = new();


            HomeSlider.ImageUrl = createHomeSliderVM.Photo.SaveImage(webHostEnvironment, "assets/img");
            HomeSlider.CreatedAt = DateTime.Now;
            HomeSlider.AddedBy = createHomeSliderVM.AddedBy;

            _context.HomeSliders.Add(HomeSlider);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existHomeSlider = _context.HomeSliders.FirstOrDefault(s => s.Id == id);
            if (existHomeSlider == null) return NotFound();

            return View(new UpdateHomeSliderVM
            {
                ImageUrl = existHomeSlider.ImageUrl

            });

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdateHomeSliderVM updateHomeSliderVM)
        {
            var existHomeSlider = _context.HomeSliders.FirstOrDefault(s => s.Id == id);
            if (existHomeSlider == null) return NotFound();

            if (updateHomeSliderVM.Photo != null)
            {
                if (updateHomeSliderVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "not image....");
                    return View(updateHomeSliderVM);

                }
                if (updateHomeSliderVM.Photo.CheckImageSize(1000))
                {


                    ModelState.AddModelError("Photo", "BOYUK OLCU....");
                    return View(updateHomeSliderVM);
                }

                string path = Path.Combine(webHostEnvironment.WebRootPath, "img", existHomeSlider.ImageUrl);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
                existHomeSlider.ImageUrl = updateHomeSliderVM.Photo.SaveImage(webHostEnvironment, "assets/img");
                existHomeSlider.AddedBy = updateHomeSliderVM.AddedBy;
                existHomeSlider.UpdatedAt = DateTime.Now;



                _context.SaveChanges();

            }

            return RedirectToAction("Index");


        }
    }
}
