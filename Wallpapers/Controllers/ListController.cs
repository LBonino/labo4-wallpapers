using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;
using Wallpapers.ViewModels;
using System.Security.Claims;

namespace Wallpapers.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
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
                    tag,
                    null
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
                tag,
                null
            ));
        }


        public IActionResult Favorites(int page, int tag)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
                    tag,
                    userId
                ));
            }

            if (page == 0 && tag == 0)
            {
                return RedirectToRoute(sortingMethod, new { page = 1 });
            }

            return View("Index", new ListViewModel
            (
                _context,
                sortingMethod,
                page,
                tag,
                userId
            ));
        }


        [HttpPost]
        public IActionResult ToggleFavorite(string userId, int postId)
        {
            var possibleFav = _context.Favorite
                .Where(f => f.UserId == userId && f.PostId == postId)
                .SingleOrDefault();

            if (possibleFav == null)
            {
                _context.Favorite.Add(new Favorite
                {
                    UserId = userId,
                    PostId = postId,
                });
            }
            else
            {
                _context.Favorite.Remove(possibleFav);
            }
            
            _context.SaveChanges();

            return Redirect($"~/w/{postId}");
        }
    }
}
