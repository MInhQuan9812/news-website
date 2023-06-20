using news24h.Models;

namespace news24h.Repository
{
    public class PostRepository
    {
        private News24hContext _context;

        public PostRepository(News24hContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public IQueryable<Post> AllPost()
        {
            IQueryable<Post> query= _context.Posts.AsQueryable();
            return query;
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
        }

        public void UpdatePost(Post post)
        {
            _context.Entry(post).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public Post FindById(int id)
        {
            return _context.Posts.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
