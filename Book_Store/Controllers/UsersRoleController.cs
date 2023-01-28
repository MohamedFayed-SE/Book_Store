/*using AspNetCore*/
using BAL.Models;
using BLL.Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.AccessControl;

namespace Book_Store.Controllers
{
    public class UsersRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task <IActionResult> Index()
        {
            var Users =  _userManager.Users.Select(usr => new UserRoleVm()
            {
                UserId = usr.Id,
                UserName = usr.UserName,
                UserRoles = _roleManager.Roles.Select(r => new CheckBox()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(usr, r.Name).Result
                }).ToList()

            }).ToList();


            
            
            
            return View(Users);
        }
        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var Roles = await _roleManager.Roles.ToListAsync();
            var Result = new UserRoleVm()
            {
                UserId = user.Id,
                UserName = user.Email,
                UserRoles = Roles.Select(r => new CheckBox()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result

                }).ToList()
            };
           





            return View(Result);
        }
        [HttpPost]
        public async Task<IActionResult> ManageRoles(UserRoleVm model)
        {
            var Result = model;

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();
            var userRoles =await  _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            var SelectedRoles = model.UserRoles.Where(sr => sr.IsSelected==true).Select(sr => sr.Name).ToList();
           await _userManager.AddToRolesAsync(user, SelectedRoles);




            return RedirectToAction("Index");   
        }


    }
}
