using System.ComponentModel.DataAnnotations;

namespace news24h.Models
{
    public class PostTopic
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual Post Post { get; set; }
    }
}
