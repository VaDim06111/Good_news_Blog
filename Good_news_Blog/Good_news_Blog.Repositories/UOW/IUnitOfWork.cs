using Core;
using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> News { get; }       

        void Save();
        Task SaveAsync();
    }
}
