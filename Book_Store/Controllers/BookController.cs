using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Book_Store.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var Result=  await _bookService.GetBooksAsync();
            return View(Result);
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookDTo book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = await _bookService.AddAsync(book);
                    return RedirectToAction("Index");
                }
                else
                    throw new Exception("Data Is Not Valid");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(book);
            }
            return View();
        }
    }
}
