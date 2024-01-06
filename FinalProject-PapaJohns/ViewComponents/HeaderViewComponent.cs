using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllUpBackEnd.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
         


            var setting = _context.Settings.ToDictionary(s=>s.Key, s=>s.Value);
            
            return View( await Task.FromResult(setting));


        }




    }
}
