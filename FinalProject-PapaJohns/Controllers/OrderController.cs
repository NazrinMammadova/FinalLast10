using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_PapaJohns.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User != null)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var currentUserId = currentUser.Id;


                    var salesForCurrentUser = _context.Sales
                          .Include(s => s.AppUser)
                          .Include(s => s.SaleProducts)
                              .ThenInclude(sp => sp.Product) 
                          .Where(s => s.AppUserId == currentUserId).OrderByDescending(p=>p.CreatedAt)
                          .ToList();


                    return View(salesForCurrentUser);
                }
                else
                {
                   
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
               
                return RedirectToAction("Login", "Account");
            }
        }




    }
}
