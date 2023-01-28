using BAL.Models;
using BLL.Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Book_Store.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
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
                    string fileName = "";
                    if (model.Photo!=null)
                    {
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        FileInfo fileInfo = new FileInfo(model.Photo.FileName);
                         fileName = model.Photo.FileName + fileInfo.Extension;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            model.Photo.CopyTo(stream);
                        }
                        model.UserPhoto = fileName;
                    }
                    //create folder if not exist
                    
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
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

           
               

        }

        public async Task<IActionResult> LogIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                        throw new Exception("User Is Not Exist");
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (result.Succeeded)
                        return RedirectToAction("Home", "Index");
                    else
                        throw new Exception("User Cannot Sign In");

                }
                else
                    throw new Exception("Data IS Not Valid");
            } catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
