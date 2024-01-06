using FinalProject_PapaJohns.Extension;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.HomeSliderVM;
using FinalProject_PapaJohns.ViewModels.Admin.PizzaPicturesVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class PizzaPicturesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PizzaPicturesController(AppDbContext context, IWebHostEnvironment webHostEnvironment = null)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var pizzaPictures = _context.pizzaPictures.ToList();
            return View(pizzaPictures);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existPizzaPicture = _context.pizzaPictures.FirstOrDefault(x => x.Id == id);
            if (existPizzaPicture == null) return NotFound();
            string path = Path.Combine(webHostEnvironment.WebRootPath, "img", existPizzaPicture.ImageUrl);
            if (System.IO.File.Exists(path))

            {
                System.IO.File.Delete(path);

            }
            _context.pizzaPictures.Remove(existPizzaPicture);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreatePizzaPicturesVM CreatePizzaPicturesVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "bosh qoyma");
                return View();

            }
            if (CreatePizzaPicturesVM.Photo.IsImage())
            {

                ModelState.AddModelError("Photo", "only image");
                return View();
            }
            if (CreatePizzaPicturesVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "sekil boyukdur");
                return View();
            }
            PizzaPicture PizzaPicture = new();


            PizzaPicture.ImageUrl = CreatePizzaPicturesVM.Photo.SaveImage(webHostEnvironment, "assets/img");
            PizzaPicture.CreatedAt = DateTime.Now;
            PizzaPicture.AddedBy = CreatePizzaPicturesVM.AddedBy;

            _context.pizzaPictures.Add(PizzaPicture);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existPizzaPicture = _context.pizzaPictures.FirstOrDefault(s => s.Id == id);
            if (existPizzaPicture == null) return NotFound();

            return View(new UpdatePizzaPicturesVM
            {
                ImageUrl = existPizzaPicture.ImageUrl

            });

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdatePizzaPicturesVM UpdatePizzaPicturesVM)
        {
            var ExistPizzaPicture = _context.pizzaPictures.FirstOrDefault(s => s.Id == id);
            if (UpdatePizzaPicturesVM == null) return NotFound();

            if (UpdatePizzaPicturesVM.Photo != null)
            {
                if (UpdatePizzaPicturesVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "not image....");
                    return View(UpdatePizzaPicturesVM);

                }
                if (UpdatePizzaPicturesVM.Photo.CheckImageSize(1000))
                {


                    ModelState.AddModelError("Photo", "BOYUK OLCU....");
                    return View(UpdatePizzaPicturesVM);
                }

                string path = Path.Combine(webHostEnvironment.WebRootPath, "img", ExistPizzaPicture.ImageUrl);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
                ExistPizzaPicture.ImageUrl = UpdatePizzaPicturesVM.Photo.SaveImage(webHostEnvironment, "assets/img");
                ExistPizzaPicture.AddedBy = UpdatePizzaPicturesVM.AddedBy;
                ExistPizzaPicture.UpdatedAt = DateTime.Now;



                _context.SaveChanges();

            }

            return RedirectToAction("Index");


        }

    }
}
