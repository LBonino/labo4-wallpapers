using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wallpapers.Data;
using Wallpapers.Models;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TagController(            ApplicationDbContext context,            UserManager<ApplicationUser> userManager        )        {
            _context = context;
            _userManager = userManager;
        }

         public IActionResult Index(int id)
         {
             return View(
                 new TagViewModel(id, _context)
             );
         }

        public IActionResult Tags()
        {
            return View(new TagsViewModel(_context));
        }
    }
}
