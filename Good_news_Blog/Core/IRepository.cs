using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        void Add(T obj);
        void AddRange(IEnumerable<T> objects);
        Task AddAsync(T obj);
        Task AddRangeAsync(IEnumerable<T> objects);
        void Update(T obj);
        void Delete(object id);
        List<T> ToList();
        IEnumerable<T> Where(Func<T, bool> predicate);
        T FirstOrDefault(IEnumerable<T> obj);
        IQueryable<T> AsQueryable();
        IEnumerable<T> OrderBy(Func<T, string> predicate);
        IEnumerable<T> OrderByDescending(Func<T, string> predicate);
        IEnumerable<T> Take(int count);
    }

}
