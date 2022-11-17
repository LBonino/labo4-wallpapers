using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class PostViewModel
    {
        private readonly int _postId;
        private readonly ApplicationDbContext _context;

        public PostViewModel(int postId, ApplicationDbContext context)
        {
            _postId = postId;
            _context = context;
        }

        public Post Post
        {
            get
            {
                return _context.Posts
                    .Include(p => p.Image)
                    .Include(p => p.User)
                    .Include(p => p.Favorites)
                    .SingleOrDefaultAsync(p => p.PostId == _postId)
                    .Result;
            }
        }

        public List<Tag> Tags
        {
            get
            {
                var tags =
                    from tag in _context.Tags
                    join postTags in _context.PostTags on tag.TagId equals postTags.TagId
                    where postTags.PostId == _postId
                    select tag;

                return tags.ToList();
            }
        }
    }
}
