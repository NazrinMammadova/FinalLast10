
using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.Services;

using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProject_PapaJohns.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context, IBasketService basketService, UserManager<AppUser> userManager)
        {
            _context = context;
            _basketService = basketService;
            _userManager = userManager;
        }

        public IActionResult Add(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            if (id == null) return NotFound();
            var product = _context.Products

                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var products = CheckBasket();
            CheckItemInBasket(products, product.Id);


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("showBasket", "basket");

        }
        public IActionResult ShowBasket()
        {

            string basket = Request.Cookies["basket"];
            List<BasketVM> products = new();
            if (basket == null) return View(products);

            products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            foreach (var basketProduct in products)
            {
                var dbProduct = _context.Products
                    .FirstOrDefault(p => p.Id == basketProduct.Id);
                basketProduct.Name = dbProduct.Name;
                basketProduct.Price = dbProduct.Price;
                basketProduct.ImageUrl = dbProduct.ImageUrl;

            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return View(products);


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            _basketService.Delete(id.Value);

            return RedirectToAction("ShowBasket");

        }
        public IActionResult Increase(int id)
        {
            _basketService.Increase(id);

            return RedirectToAction("ShowBasket");
        }


        public IActionResult Decrease(int id)
        {
            _basketService.Decrease(id);
            return RedirectToAction("ShowBasket");


        }
        private List<BasketVM> CheckBasket()
        {
            List<BasketVM> products;
            string basket = Request.Cookies["basket"];
            if (basket == null)
            {
                products = new List<BasketVM>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);


            }
            return products;
        }

        private void CheckItemInBasket(List<BasketVM> products, int id)
        {
            var existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = id;
                basketVM.BasketCount = 1;
                products.Add(basketVM);
            }
            else
            {
                existProduct.BasketCount++;
            }

        }
      
    }
}