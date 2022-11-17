using Microsoft.AspNetCore.Mvc;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            return View(model);
        }
    }
}
