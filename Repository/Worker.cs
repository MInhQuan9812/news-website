using news24h.Models;

namespace news24h.Repository
{
    public class Worker
    {

        private readonly News24hContext _context;
        private PostRepository _postRepository;
        private TopicRepository _topicRepository;
        private UserRepository _userRepository;


        public Worker(News24hContext context)
        {
            _context = context;
        }

        public PostRepository postRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    if (_context != null)
                    {
                        _postRepository = new PostRepository(_context);
                    }
                }
                return _postRepository;
            }           
        } 

        public TopicRepository topicRepository
        {
            get
            {
                if (_topicRepository == null)
                {
                    if (_context != null)
                    {
                        _topicRepository = new TopicRepository(_context);
                    }
                }
                return _topicRepository;
            }
        }

        public UserRepository userRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    if (_context != null)
                    {
                        _userRepository = new UserRepository(_context);
                    }
                }
                return _userRepository;
            }
        }


    }
}
