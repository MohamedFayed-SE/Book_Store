using BAL.Models;
using BLL.Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var Result = await _userManager.Users.ToListAsync();
            return View(Result);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegistrationDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var IsExist= await _userManager.FindByEmailAsync(model.Email);
                    if (IsExist!=null)
                        throw new Exception("this is Email Is Aleady Registration");
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    FileInfo fileInfo = new FileInfo(model.Photo.FileName);
                    string fileName = model.Photo.FileName + fileInfo.Extension;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }
                    model.UserPhoto = fileName;
                    var User = new ApplicationUser()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Photo = model.Photo,
                        UserPhoto = fileName,
                        isConfirmed = true,
                        UserName = model.Email


                    };
                    var Result = await _userManager.CreateAsync(User, model.Password);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        return View(model);
                }
                else
                    throw new Exception("Data Is Not Valid");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }

           
               

        }
        /* else
         {
             ModelState.AddModelError("","Data Is NOt Valid");
             return View(User);
         }*/



    }
}
