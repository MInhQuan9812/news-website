namespace news24h.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Post Post { get; set; }
    }
}
