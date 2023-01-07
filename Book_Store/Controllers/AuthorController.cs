using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAutherService _AuthorService;

        public AuthorController(IAutherService authorService)
        {
            _AuthorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var Result = await _AuthorService.GetAuthorsAsync();
            return View(Result);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = await _AuthorService.AddAsync(author);
                    return RedirectToAction("Index");
                }
                else
                    throw new Exception("Data Is Not Valid");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(author);
            }
            return View();
        }
    }
}

