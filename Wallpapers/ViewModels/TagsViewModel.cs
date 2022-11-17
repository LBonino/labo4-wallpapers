using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class TagsViewModel
    {
        private readonly ApplicationDbContext _context;

        public TagsViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tag> Tags
        {
            get
            {
                return _context.Tags
                    .Include(t => t.User)
                    .Include(t => t.PostTags)
                    .ToList();
            }
        }
    }
}
