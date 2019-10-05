using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsFromOnliner
{
    public interface INewsOnlinerParser
    {
        IEnumerable<News> GetFromUrl(string url);        
        News GetById(Guid id);
        Task<News> GetByIdAsync(Guid id);
        bool Add(News news);
        Task<bool> AddAsync(News news);
    }
}
