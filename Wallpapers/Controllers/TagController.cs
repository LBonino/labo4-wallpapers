using Microsoft.AspNetCore.Mvc;

namespace Wallpapers.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
