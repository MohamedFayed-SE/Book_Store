using Microsoft.AspNetCore.Mvc;
using BLL.Services;

namespace Book_Store.Controllers
{
    public class AuthrizationController : Controller
    {
        
        public IActionResult Index()
        {
            var result = PermationsService.getAllModulesWithActions();
            return View(result);
        }
    }
}
