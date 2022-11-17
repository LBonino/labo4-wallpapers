using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallpapers.Data;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index(int id)
        {

            return View(
                new PostViewModel(id, _context)
            );
        }
    }
}
