using System.ComponentModel.DataAnnotations;

namespace news24h.Models
{
    public class Topic
    {

        private IEnumerable<PostTopic> _postTopics;

        [Key]
        public int TopicId { get; set; }

        public string TopicName { get; set; }

        public virtual IEnumerable<PostTopic> PostTopics
        {
            get => _postTopics ??= new List<PostTopic>();
            set => _postTopics = value;
        }
    }
}
