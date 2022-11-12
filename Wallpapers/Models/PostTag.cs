using Wallpapers.Models;

namespace Wallpapers.Models
{
    public class PostTag
    {
        public int PostTagId { get; set; }
        
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
