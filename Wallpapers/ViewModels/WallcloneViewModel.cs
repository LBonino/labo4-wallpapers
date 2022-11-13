using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wallpapers.Data;
using Microsoft.Extensions.Caching.Memory;
using Wallpapers.Models;

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

        public DbSet<Post> Posts
        {
            get
            {
                return _context.Posts;
            }
        }

        public DbSet<Tag> Tags
        {
            get
            {
                return _context.Tags;
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
