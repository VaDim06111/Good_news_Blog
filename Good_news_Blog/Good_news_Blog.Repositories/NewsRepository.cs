using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
