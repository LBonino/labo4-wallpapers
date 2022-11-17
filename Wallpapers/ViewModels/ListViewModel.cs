using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using Wallpapers.Data;
using Wallpapers.Models;

namespace Wallpapers.ViewModels
{
    public class ListViewModel
    {
        private readonly ApplicationDbContext _context;
        private readonly string _sortingMethod;
        private readonly int _pageSize = 24;
        private readonly int _totalPages;
        private readonly int _tagIdToFilter;
        private readonly IQueryable<Post> _posts;
        public readonly int _pageIndex;

        public ListViewModel(
            ApplicationDbContext context,
            string sortingMethod,
            int page,
            int tagIdToFilter
        )
        {
            _context = context;
            _sortingMethod = sortingMethod;
            _tagIdToFilter = 
            _pageIndex = (page == 0) ? 1 : page;
            _tagIdToFilter = tagIdToFilter;

            _posts = (tagIdToFilter != 0)
                ? _context.PostTags
                .Include(p => p.Post.Image)
                .Include(p => p.Post.Favorites)
                .Where(pt => pt.TagId == tagIdToFilter)
                .Select(pt => pt.Post)

                : _context.Posts
                .Include(p => p.Image)
                .Include(p => p.Favorites);

            _totalPages = (int)Math.Ceiling(_posts.Count() / (double)_pageSize);
        }

        

        public List<Post> PostsPage
        {
            get
            {
                if (_tagIdToFilter != 0 && _posts.Count() == 0)
                {
                    return _posts.ToList();
                }

                switch (_sortingMethod)
                {
                    case "latest": 
                        return _posts
                            .OrderByDescending(p => p.SubmissionDate)
                            .Skip(_pageSize * _pageIndex - _pageSize)
                            .Take(_pageSize)
                            .ToList();

                    case "random":
                        return _posts
                            .OrderBy(p => Guid.NewGuid())
                            .Take(_pageSize)
                            .ToList();

                    default:
                        return _posts
                            .OrderByDescending(p => p.Favorites.Count())
                            .Skip(_pageSize * _pageIndex - _pageSize)
                            .Take(_pageSize)
                            .ToList();
                }
            }
        }

        public string SortingMethod
        {
            get
            {
                return _sortingMethod;
            }
        }

        public bool HasPreviousPage => _pageIndex > 1;
        public bool HasNextPage => _pageIndex < _totalPages;
    }
}
