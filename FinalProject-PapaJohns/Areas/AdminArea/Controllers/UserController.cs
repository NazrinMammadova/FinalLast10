using FinalProject_PapaJohns.Models;
using FinalProject_PapaJohns.ViewModels.Admin.RoleVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_PapaJohns.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string search)
        {

            var users = search == null ? _userManager.Users.ToList() :
                  _userManager.Users
                  .Where(u => u.UserName.ToLower().Contains(search.ToLower())).ToList();



            return View(users);


        }


        public async Task<IActionResult> ChangeStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("index");
        }


        public async Task<IActionResult> Update(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UpdateRoleVM updateRoleVM = new UpdateRoleVM
            {
                Roles = _roleManager.Roles.ToList(),
                UserRoles = await _userManager.GetRolesAsync(user),
                User = user

            };
            return View(updateRoleVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(string id, List<string> roles)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userOldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userOldRoles);


            await _userManager.AddToRolesAsync(user, roles);
            return RedirectToAction("Index");
        }





        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }





    }
}
