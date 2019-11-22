using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IParserCQS<T> where T : class
    {
        IEnumerable<T> GetFromUrl();
        Task<bool> AddRangeAsync(IEnumerable<T> objects);
    }
}
