using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Wallpapers.Data;
using Wallpapers.Models;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class DeleteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public IActionResult Index(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var post = _context.Posts
                .Where(p => p.PostId == postId && p.UserId == userId)
                .First();

            var postTags = _context.PostTags
                .Where(pt => pt.PostId == postId)
                .ToList();

            var favorites = _context.Favorite
                .Where(f => f.PostId == postId)
                .ToList();

            var image = _context.Images
                .Where(i => i.PostId == postId)
                .First();

            _context.Favorite.RemoveRange(favorites);
            _context.PostTags.RemoveRange(postTags);
            _context.Posts.Remove(post);
            _context.Images.Remove(image);
            _context.SaveChanges();

            return Redirect("/latest");
        }
    }
}
