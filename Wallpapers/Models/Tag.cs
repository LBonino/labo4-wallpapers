using System;
using System.Collections.Generic;
using Wallpapers.Models;

namespace Wallpapes.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public DateTime AdditionDate { get; set; }
        
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
    }
}
