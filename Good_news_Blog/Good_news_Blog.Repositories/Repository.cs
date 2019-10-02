using Core;
using Good_news_Blog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;

        public Repository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _table = _context.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _table.AsQueryable();
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public T FirstOrDefault(IEnumerable<T> obj)
        {
            return obj.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public void Add(T obj)
        {
            _table.Add(obj);
        }

        public async Task AddAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public void AddRange(IEnumerable<T> objects)
        {
            _table.AddRange(objects);
        }

        public async Task AddRangeAsync(IEnumerable<T> objects)
        {
            await _table.AddRangeAsync(objects);
        }

        public List<T> ToList()
        {
            return _table.ToList();
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return _table.Where(predicate);
        }

        public IEnumerable<T> OrderBy(Func<T, string> predicate)
        {
            return _table.OrderBy(predicate);
        }

        public IEnumerable<T> OrderByDescending(Func<T, string> predicate)
        {
            return _table.OrderByDescending(predicate);
        }

        public IEnumerable<T> Take(int count)
        {
            return _table.Take(count);
        }
    }
}
