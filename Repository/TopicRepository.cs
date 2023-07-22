using news24h.Models;

namespace news24h.Repository
{
    public class TopicRepository
    {

        private News24hContext _context;

        public TopicRepository(News24hContext context)
        {
            _context = context;
        }

        public void AddTopic(Topic topic)
        {
            _context.Topics.Add(topic);
        }

        public IQueryable<Topic> AllTopic()
        {
            IQueryable<Topic> query = _context.Topics.AsQueryable();
            return query;
        }

        public void DeleteTopic(Topic topic)
        {
            _context.Topics.Remove(topic);
        }

        public void UpdateTopic(Topic topic)
        {
            _context.Entry(topic).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public Topic FindById(int id)
        {
            return _context.Topics.Find(id);
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
