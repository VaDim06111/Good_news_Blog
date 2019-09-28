using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T obj);
        void Update(T obj);
        void Delete(object id);
        List<T> ToList();
        IEnumerable<T> Where(Func<T,bool> predicate);
        T FirstOrDefault(IEnumerable<T> obj);
        IQueryable<T> AsQueryable();
    }
}
