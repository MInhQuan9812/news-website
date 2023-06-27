using news24h.Models;

namespace news24h.Repository
{
    public class UserRepository
    {

        private readonly News24hContext _context;

        public UserRepository(News24hContext context)
        {
            _context=context;
        }

        public User AddUser(User user)
        {
           return _context.Users.Add(user).Entity;
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);
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
