using FinalProject_PapaJohns.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existContact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if (existContact == null) return NotFound();

            _context.Contacts.Remove(existContact);

            _context.SaveChanges();


            return RedirectToAction("Index");
        }



    }
}
