using Core;
using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Comment> _commentRepository;

        public UnitOfWork(ApplicationDbContext context,
            IRepository<News> news,
            IRepository<Comment> comments)
            
        {
            _context = context;
            _newsRepository = news;
            _commentRepository = comments;

        }

        public IRepository<News> News => _newsRepository;

        public IRepository<Comment> Comments => _commentRepository;

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
