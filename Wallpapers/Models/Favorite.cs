using System.Data;

namespace Wallpapers.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
