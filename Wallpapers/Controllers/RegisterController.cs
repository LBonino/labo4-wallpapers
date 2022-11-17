using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wallpapers.Models;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var possibleExistingEmail = await userManager.FindByEmailAsync(model.Email);
            if (possibleExistingEmail != null)
            {
                ModelState.AddModelError("Email", "Ya hay un usuario registrado con ese email");
                return View(model);
            }

            var possibleExistingUserName = await userManager.FindByNameAsync(model.Username);
            if (possibleExistingUserName != null)
            {
                ModelState.AddModelError("Username", "Ya hay un usuario registrado con ese nombre");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                NormalizedUserName = model.Username.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Count() > 0)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("message", error.Description);
                    }
                }

                return View(model);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
