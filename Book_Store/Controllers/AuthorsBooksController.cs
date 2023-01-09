using BLL.Dtos;
using BLL.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class AuthorsBooksController : Controller
    {
        private readonly IAuthorsBooksService _authorsBooksService;
        public AuthorsBooksController(IAuthorsBooksService authorsBooksService)
        {
              _authorsBooksService = authorsBooksService;
        }
        
        public async Task<IActionResult> Index()
        {
            var Result= await _authorsBooksService.GetAuthorsBooksAsync();
            return View(Result);
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuthorsBooksDto authorBook)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   await _authorsBooksService.AddAsync(authorBook);
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    throw new Exception("Data Is Not Valid");
                }

            }catch( Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View(authorBook);
            }
            
            
        }
    }
}
