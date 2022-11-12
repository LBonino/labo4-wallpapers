using System.ComponentModel.DataAnnotations;

namespace Wallpapers.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        public string Filename { get; set; }
        [Required]
        public string Extension { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long SizeInBytes { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
