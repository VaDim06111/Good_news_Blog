using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Models
{
    public class IndexViewModel
    {
        public IEnumerable<News> News { get; set; }
        public NewsPageViewModel PageViewModel { get; set; }              
    }
}
