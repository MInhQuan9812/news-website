using news24h.Models;

namespace news24h.Repository
{
    public class CommentRepository
    {

        private News24hContext _context;

        public CommentRepository(News24hContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comment.Add(comment);
        }

        public IQueryable<Comment> AllComment()
        {
            IQueryable<Comment> query = _context.Comment.AsQueryable();
            return query;
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comment.Remove(comment);
        }

        public void UpdatePost(Comment comment)
        {
            _context.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public Comment FindById(int id)
        {
            return _context.Comment.Find(id);
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
