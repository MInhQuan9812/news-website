using System.ComponentModel.DataAnnotations;

namespace news24h.Models
{
    public class User
    {

        private IEnumerable<Post>? _posts;

        private IEnumerable<Comment>? _comments;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Fullname { get; set; }


        public string? Role { get; set; }


        public virtual IEnumerable<Post>? Posts
        {
            get => _posts ??= new List<Post>();
            set => _posts = value;
        }

        public virtual IEnumerable<Comment>? Comments
        {
            get => _comments ??= new List<Comment>();
            set => _comments = value;
        }
    }
}
