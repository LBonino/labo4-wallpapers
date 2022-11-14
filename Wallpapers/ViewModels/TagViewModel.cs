using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class TagViewModel
    {
        private readonly int _tagId;
        private readonly ApplicationDbContext _context;

        public TagViewModel(int tagId, ApplicationDbContext context)
        {
            _tagId = tagId;
            _context = context;
        }

        public Tag Tag
        {
            get
            {
                return _context.Tags
                    .Include(t => t.PostTags)
                    .Include(t => t.User)
                    .SingleOrDefaultAsync(t => t.TagId == _tagId)
                    .Result;
            }
        }

        public List<PostTag> EightRandomPostTags
        {
            get
            {
                return _context.PostTags
                    .Include(p => p.Post.Image)
                    .Include(p => p.Post.Favorites)
                    .Where(p => p.TagId == _tagId)
                    .OrderBy(p => Guid.NewGuid())
                    .Take(8)
                    .ToList();
            }
        }
    }
}
