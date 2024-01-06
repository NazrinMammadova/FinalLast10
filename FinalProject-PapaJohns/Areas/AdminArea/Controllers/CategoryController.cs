using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.CategoryVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories  = _context.Categories.ToList();
            return View(categories);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();



            return RedirectToAction("Index");


        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateCategoryVM category)
        {
            if (!ModelState.IsValid) return View();
            var isExistCategoryWithName = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExistCategoryWithName)
            {
                ModelState.AddModelError("Name", "eyni adli ola bilmez");
                return View();
            }




            Category newCategory = new();
            newCategory.Name = category.Name;
            newCategory.CreatedAt = DateTime.Now;
            newCategory.AddedBy = category.AddedBy;

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();
            UpdateCategoryVM updateCategoryVM = new()
            {
                Name = existCategory.Name,
                AddedBy = existCategory.AddedBy,


            };

            return View(updateCategoryVM);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdateCategoryVM updateCategoryVM)
        {
            if (!ModelState.IsValid) return NotFound();
            var existCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            var isExistCategoryWithName = _context.Categories.Any(c => c.Name.ToLower() == updateCategoryVM.Name.ToLower());
            if (isExistCategoryWithName)
            {
                ModelState.AddModelError("Name", "eyni adli ola bilmez");
                return View();
            }
            existCategory.Name = updateCategoryVM.Name;
            existCategory.UpdatedAt = DateTime.Now;
            existCategory.AddedBy = updateCategoryVM.AddedBy;

            _context.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
