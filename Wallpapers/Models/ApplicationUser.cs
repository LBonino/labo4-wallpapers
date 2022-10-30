using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Wallpapes.Models;

namespace Wallpapers.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
