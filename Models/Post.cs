using System.ComponentModel.DataAnnotations;

namespace news24h.Models
{
    public class Post
    {


        private IEnumerable<Comment>? _comments;


        private IEnumerable<PostTopic> _postTopics;

        public int Id { get; set; }

        public int? CreatorId { get; set; }

        public string? PostTitle { get; set; }

        public string? PostContent { get; set; }

        public string? PostTopic { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? LastEdited { get; set; }

        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        public string? ImageUrl { get; set; }

        public virtual User? Creator { get; set; }

        public virtual IEnumerable<PostTopic> PostTopics
        {
            get => _postTopics ??= new List<PostTopic>();
            set => _postTopics = value;
        }

        public int Priority { get; set; }

        public virtual IEnumerable<Comment>? Comments
        {
            get => _comments ??= new List<Comment>();
            set => _comments = value;
        }
    }
}
