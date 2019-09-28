using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;

        public UnitOfWork(ApplicationDbContext context,
            IRepository<News> news,
            IRepository<Role> roles,
            IRepository<User> users,
            IRepository<UserRole> userRoles)
        {
            _context = context;
            _newsRepository = news;
            _roleRepository = roles;
            _userRepository = users;
            _userRoleRepository = userRoles;

        }

        public IRepository<News> News => _newsRepository;

        public IRepository<Role> Roles => _roleRepository;

        public IRepository<User> Users => _userRepository;

        public IRepository<UserRole> UserRoles => _userRoleRepository;

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
