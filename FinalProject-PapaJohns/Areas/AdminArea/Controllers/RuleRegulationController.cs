using FinalProject_PapaJohns.DAL;
using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.PizzaServicesVM;
using FinalProject_PapaJohns.ViewModels.Admin.RuleRegulationVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class RuleRegulationController : Controller
    {
        private readonly AppDbContext _context;

        public RuleRegulationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rulergulation = _context.RuleRegulations.ToList();
            return View(rulergulation);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existRuleRegulation = _context.RuleRegulations.FirstOrDefault(x => x.Id == id);
            if (existRuleRegulation == null) return NotFound();
            _context.RuleRegulations.Remove(existRuleRegulation);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateRuleRegulationVM CreateRuleRegulationVM)
        {


            RuleRegulation RuleRegulation = new();


            RuleRegulation.CreatedAt = DateTime.Now;
            RuleRegulation.AddedBy = CreateRuleRegulationVM.AddedBy;
            RuleRegulation.Desc = CreateRuleRegulationVM.Desc;
            RuleRegulation.Name = CreateRuleRegulationVM.Name;

            _context.RuleRegulations.Add(RuleRegulation);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var existRuleRegulation = _context.RuleRegulations.FirstOrDefault(s => s.Id == id);
            if (existRuleRegulation == null) return NotFound();

            return View();

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int id, UpdateRuleRegulationVM UpdateRuleRegulationVM)
        {
            var existRuleRegulation = _context.RuleRegulations.FirstOrDefault(s => s.Id == id);
            if (existRuleRegulation == null) return NotFound();




            existRuleRegulation.AddedBy = UpdateRuleRegulationVM.AddedBy;
            existRuleRegulation.UpdatedAt = DateTime.Now;
            existRuleRegulation.Desc = UpdateRuleRegulationVM.Desc;
            existRuleRegulation.Name = UpdateRuleRegulationVM.Name;



                _context.SaveChanges();

   
            return RedirectToAction("Index");


        }

    }
}
