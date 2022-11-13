using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class HomeViewModel : WallcloneViewModel
    {
        public HomeViewModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        ) : base(context, userManager)
        {    
        }

        public List<Post> RandomPostsToDisplay
        {
            get
            {
                return Posts
                    .Include(p => p.Image)
                    .OrderBy(p => Guid.NewGuid())
                    .Take(15)
                    .ToList();
            }
        }
    }
}
