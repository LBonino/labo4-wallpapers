using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallpapers.Data;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page, int tag)
        {
            // Método de ordenamiento se incluye en la misma ruta
            var sortingMethod = HttpContext.Request.Path
                .ToString()
                .ToLower()
                .Replace("/", "");

            if (sortingMethod == "random")
            {
                return View(new ListViewModel
                (
                    _context,
                    sortingMethod,
                    page,
                    tag
                ));
            }

            if (page == 0 && tag == 0)
            {
                return RedirectToRoute(sortingMethod, new {page = 1});
            }

            return View(new ListViewModel
            (
                _context,
                sortingMethod,
                page,
                tag
            ));
        }
    }
}
