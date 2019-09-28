using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> News { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<UserRole> UserRoles { get; }      

        void Save();
        Task SaveAsync();
    }
}
