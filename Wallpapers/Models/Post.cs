using System;
using System.Collections.Generic;

namespace Wallpapers.Models
{
    public enum SubmissionStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class Post
    {
        public int PostId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public SubmissionStatus SubmissionStatus { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
