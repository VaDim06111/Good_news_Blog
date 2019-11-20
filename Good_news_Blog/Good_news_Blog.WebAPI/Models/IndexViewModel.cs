using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.Data;

namespace Good_news_Blog.WebAPI.Models
{
    public class IndexViewModel
    {
        public IEnumerable<News> News { get; set; }
        public NewsPageViewModel PageViewModel { get; set; }
    }
}
