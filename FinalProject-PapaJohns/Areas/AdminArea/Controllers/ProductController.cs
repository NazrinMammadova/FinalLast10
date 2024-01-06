using FinalProject_PapaJohns.Extension;
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Helpers;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.ProductVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{

    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class ProductController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _usermanger;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> usermanger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _usermanger = usermanger;
        }

        public IActionResult Index(int page = 1, int take = 7)

        {
            var query = _context.Products.AsQueryable();

            var products = query.Include(p => p.Category)
                .Include(p => p.Category)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();






            Pagination<Product> pagination = new(products, CalculatePageCount(query, take), page);


            return View(pagination);
        }
        private int CalculatePageCount(IQueryable<Product> query, int take)
        {
            var count = query.Count();

            return (int)Math.Ceiling((decimal)count / take);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (existProduct == null) return NotFound();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", existProduct.ImageUrl);
            if (System.IO.File.Exists(path))

            {
                System.IO.File.Delete(path);

            }
            _context.Products.Remove(existProduct);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            ViewBag.categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateProductVM createProductVM)
        {
            if (!ModelState.IsValid) return View();
            Product newProduct = new();





            if (createProductVM.Photo.IsImage())
            {


                ModelState.AddModelError("", "only image");
                return View();

            }
            if (createProductVM.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("", "duz emelli size goy");
                return View();
            }
            newProduct.ImageUrl = createProductVM.Photo.SaveImage(_webHostEnvironment, "assets/img");
            newProduct.Name = createProductVM.Name;
            newProduct.CategoryId = createProductVM.CategoryId;
            newProduct.Price = createProductVM.Price;
            newProduct.AddedBy = createProductVM.AddedBy;
            newProduct.Description = createProductVM.Desc;

            _context.Products.Add(newProduct);
            _context.SaveChanges();


            var users = _usermanger.Users.ToList();
            foreach (var user in users)
            {
                var link = Url.Action("Detail", "Product", new { Id = newProduct.Id }, Request.Scheme, Request.Host.ToString());
                var newLink = link.Replace("/AdminArea/", "/");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mammadova.nazrin2004@gmail.com", "PapaJhons");
                mail.To.Add(new MailAddress(user.Email));
                mail.Subject = "New Product";
                mail.Body = $"<a href={newLink}> yeni mehsulumuza baxa bilersiniz...... </a>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "cpai qbgb ybst rmyk");
                smtpClient.Send(mail);


            }


            return RedirectToAction("Index");



        }

        public IActionResult Update(int? id)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
         
            ViewBag.Category = new SelectList(_context.Categories.ToList(), "Id", "Name");
            UpdateProductVM updateProductVM = new UpdateProductVM();
            updateProductVM.Name = existProduct.Name;
            updateProductVM.Price = existProduct.Price;
            updateProductVM.CategoryId = existProduct.CategoryId;
            updateProductVM.UpdatedAt = DateTime.Now;
            updateProductVM.AddedBy = existProduct.AddedBy;
        
            updateProductVM.Id = existProduct.Id;
            return View(updateProductVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, UpdateProductVM updateProductVM)
        {
            if (ModelState.IsValid) return View();

            ViewBag.Category = new SelectList(_context.Categories.ToList(), "Id", "Name");
            var existProduct = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            var existProductWithName = _context.Products.Any(p => p.Name.ToLower() == updateProductVM.Name.ToLower() && p.Id != id);

            if (existProductWithName)
            {
                ModelState.AddModelError("", "This product already exists");
                return View();
            }

            if (updateProductVM.newPhoto != null)
            {
                if (!updateProductVM.newPhoto.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("", "Please choose an image file");
                    return View(updateProductVM);
                }
                if (updateProductVM.newPhoto.Length / 1024 > 5000)
                {
                    ModelState.AddModelError("", "This photo's size is larger than 5MB");
                    return View(updateProductVM);
                }

                var newUrl = Guid.NewGuid().ToString() + updateProductVM.newPhoto.FileName;
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/img", newUrl);
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    updateProductVM.newPhoto.CopyTo(file);
                }

                var oldUrl = Path.Combine(_webHostEnvironment.WebRootPath, "assets/img", existProduct.ImageUrl);
                if (System.IO.File.Exists(oldUrl))
                {
                    System.IO.File.Delete(oldUrl);
                }

                existProduct.ImageUrl = newUrl;
            }

            existProduct.Name = updateProductVM.Name;
            existProduct.UpdatedAt = DateTime.Now;
            existProduct.Price = updateProductVM.Price;
            existProduct.CategoryId = updateProductVM.CategoryId;
            existProduct.AddedBy = updateProductVM.AddedBy;
            existProduct.Description = updateProductVM.Desc;

            _context.SaveChanges();
            var users = _usermanger.Users.ToList();
            foreach (var user in users)
            {
                var link = Url.Action("Detail", "Product", new { Id = existProduct.Id }, Request.Scheme, Request.Host.ToString());
                var newLink = link.Replace("/AdminArea/", "/");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mammadova.nazrin2004@gmail.com", "PapaJhons");
                mail.To.Add(new MailAddress(user.Email));
                mail.Subject = "New Product";
                mail.Body = $"<a href={newLink}> mehsulumuzun  yenilendi linke kecid edib baxa bilersiniz...... </a>";
                mail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "cpai qbgb ybst rmyk");
                smtpClient.Send(mail);


            }

            return RedirectToAction("index", "product");
        }


    }





}




    


