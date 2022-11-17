using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
//using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Wallpapers.Data;
using Wallpapers.Models;
using Wallpapers.ViewModels;

namespace Wallpapers.Controllers
{
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(new UploadViewModel());
        }

        [HttpPost]
        public IActionResult Index(UploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tag = _context.Tags
                .Where(t => t.Name.ToUpper() == model.TagName.ToUpper())
                .FirstOrDefault();

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = model.TagName,
                    UserId = userId,
                    AdditionDate = DateTime.Now,
                };

                _context.Tags.Add(tag);
            }

            var folderToSaveWallpaperPath = Path.Combine(
                        _env.WebRootPath,
                        "wallpapers"
                    );

            var fullSizePath = Path.Combine(folderToSaveWallpaperPath, "fullsize");
            var thumbnailPath = Path.Combine(folderToSaveWallpaperPath, "thumbnail");

            Directory.CreateDirectory(fullSizePath);
            Directory.CreateDirectory(thumbnailPath);

            var post = new Post
            {
                SubmissionDate = DateTime.Now,
                SubmissionStatus = SubmissionStatus.Approved,
                UserId = userId,
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            var extension = Path.GetExtension(model.File.FileName);

            System.Drawing.Image wallpaperImage;
            System.Drawing.Image thumbImage;
            using (var stream = model.File.OpenReadStream())
            {
                wallpaperImage = System.Drawing.Image.FromStream(stream);
                var thumbWidth = 432;
                var thumbHeight = (int)Math.Round((((float)thumbWidth / wallpaperImage.Width) * wallpaperImage.Height));
                thumbImage = wallpaperImage.GetThumbnailImage(thumbWidth, thumbHeight, () => false, IntPtr.Zero);

                var image = new Image
                {
                    Width = wallpaperImage.Width,
                    Height = wallpaperImage.Height,
                    Extension = extension,
                    SizeInBytes = model.File.Length,
                    Filename = $"{post.PostId}{extension}",
                    PostId = post.PostId
                };

                _context.Images.Add(image);

                _context.PostTags.Add(new PostTag
                {
                    TagId = tag.TagId,
                    PostId = post.PostId
                });

                using (var stream2 = System.IO.File.Create(Path.Combine(fullSizePath, image.Filename)))
                {
                    wallpaperImage.Save(stream2, ImageFormat.Jpeg);
                }

                using (var stream2 = System.IO.File.Create(Path.Combine(thumbnailPath, image.Filename)))
                {
                    thumbImage.Save(stream2, ImageFormat.Jpeg);
                }
            }
            //var wallpaperImage = System.Drawing.Image.FromStream(stream);

            
           
            _context.SaveChanges();

            return Redirect($"w/{post.PostId}");
        }
    }
}
