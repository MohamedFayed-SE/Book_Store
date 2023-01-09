using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Book_Store.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var Result = await _bookService.GetBooksAsync();
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
           
        }

        public async Task<IActionResult> Update(int id)
        {
            var Book = await _bookService.GetBookByIdAsync(id);
            var Categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryList = new SelectList(Categories, "Id", "Name", Book.Id);
            return View(Book);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BookDTo book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bookService.UpdateAsync(book);
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


        }




        public async Task<IActionResult> Delete(int id)
        {
            var Book = await _bookService.GetBookByIdAsync(id);
            var Categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryList = new SelectList(Categories, "Id", "Name", Book.Id);
            return View(Book);

            
        }
        [HttpPost]
        public async Task<IActionResult> Delete(BookDTo book)
        {
                     await _bookService.DeleteAsync(book.Id);
                    return RedirectToAction("Index");
   
        }
    }
}
