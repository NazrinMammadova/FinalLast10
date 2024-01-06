using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Helpers;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int take = 6)
        {
            ShopVM shopVM = new();


            shopVM.Products = _context.Products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
            var count = _context.Products.Count();
            int pageCount = (int)Math.Ceiling((decimal)count / take);

            Pagination<Product> pagination = new(shopVM.Products, pageCount, page);

            return View(pagination);

        }







    }


    }
