using FinalProject_PapaJohns.Extension;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.ViewModels.Admin.MenuPictureVM;
using FinalProject_PapaJohns.ViewModels.Admin.PizzaPicturesVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class MenuPictureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;



        public MenuPictureController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var menuPicture = _context.MenuPictures.FirstOrDefault();
            return View(menuPicture);

        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existMenuPicture = _context.MenuPictures.FirstOrDefault(s => s.Id == id);
            if (existMenuPicture == null) return NotFound();

            return View(new UpdateMenuPictureVM
            {
                ImageUrl = existMenuPicture.MenuPhoto

            });

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdateMenuPictureVM updateMenuPictureVM)
        {
            var ExistMenuPicture = _context.MenuPictures.FirstOrDefault(s => s.Id == id);
            if (updateMenuPictureVM == null) return NotFound();

            if (updateMenuPictureVM.Photo != null)
            {
                if (updateMenuPictureVM.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "not image....");
                    return View(updateMenuPictureVM);

                }
                if (updateMenuPictureVM.Photo.CheckImageSize(1000))
                {


                    ModelState.AddModelError("Photo", "BOYUK OLCU....");
                    return View(updateMenuPictureVM);
                }

                string path = Path.Combine(webHostEnvironment.WebRootPath, "img", ExistMenuPicture.MenuPhoto);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                }
                ExistMenuPicture.MenuPhoto = updateMenuPictureVM.Photo.SaveImage(webHostEnvironment, "assets/img");
                ExistMenuPicture.AddedBy = updateMenuPictureVM.AddedBy;
                ExistMenuPicture.UpdatedAt = DateTime.Now;



                _context.SaveChanges();

            }

            return RedirectToAction("Index");


        }
    }
}
