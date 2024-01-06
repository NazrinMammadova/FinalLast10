using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
ContactVM contactVM = new ContactVM();
            contactVM.Setting = _context.Settings.ToDictionary(s=>s.Key ,s=>s.Value);
            return View(contactVM);


        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SendMessage(Contact contact)
        {
            contact.AddedBy = contact.Name;
            contact.CreatedAt = DateTime.Now;

            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}
