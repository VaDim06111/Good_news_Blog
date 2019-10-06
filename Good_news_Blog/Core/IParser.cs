using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IParser<T> where T: class
    {
        IEnumerable<T> GetFromUrl();
        bool Add(T obj);
        Task<bool> AddAsync(T obj);
        bool AddRange(IEnumerable<T> objects);
        Task<bool> AddRangeAsync(IEnumerable<T> objects);
    }
}
