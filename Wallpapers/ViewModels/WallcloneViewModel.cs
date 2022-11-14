using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wallpapers.Data;
using Microsoft.Extensions.Caching.Memory;
using Wallpapers.Models;
using System.Collections.Generic;

namespace Wallpapers.ViewModels
{
    public class WallcloneViewModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WallcloneViewModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Post> Posts
        {
            get
            {
                return _context.Posts.ToList();
            }
        }

        public List<Post> PostsWithImages
        {
            get
            {
                return _context.Posts
                    .Include(p => p.Image)
                    .ToList();
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return _context.Tags.ToList();
            }
        }

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return _userManager.Users;
            }
        }
    }
}
