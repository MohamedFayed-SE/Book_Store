using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class CategoryController : Controller
    {
       
        private readonly ICategoryService _CategoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _CategoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var Result = await _CategoryService.GetCategoriesAsync();
            return View(Result);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = await _CategoryService.AddAsync(category);
                    return RedirectToAction("Index");
                }
                else
                    throw new Exception("Data Is Not Valid");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(category);
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _CategoryService.GetById(id);

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _CategoryService.UpdateAsync(category);
                    return RedirectToAction("Index");
                }
                else
                    throw new Exception("Data Is Not Valid");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(category);
            }


        }




        public async Task<IActionResult> Delete(int id)
        {
            var category = await _CategoryService.GetById(id);

            return View(category);


        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDto category)
        {
             _CategoryService.Delete(category.Id);
            return RedirectToAction("Index");

        }
    }
}
