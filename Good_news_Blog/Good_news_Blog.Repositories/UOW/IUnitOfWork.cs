using Good_news_Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Good_news_Blog.Repositories.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> News { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<UserRole> UserRoles { get; }      

        void Save();
    }
}
