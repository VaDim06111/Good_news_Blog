using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsFromS13
{
    public interface INewsS13Parser
    {
        IEnumerable<News> GetFromUrl();             
        bool Add(News news);
        Task<bool> AddAsync(News news);
        bool AddRange(IEnumerable<News> news);       
        Task<bool> AddRangeAsync(IEnumerable<News> news);
    }
}

