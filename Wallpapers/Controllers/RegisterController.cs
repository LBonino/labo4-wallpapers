using Microsoft.AspNetCore.Mvc;

namespace Wallpapers.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
