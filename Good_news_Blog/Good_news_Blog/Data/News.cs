using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public byte IndexOfPositive { get; set; }

    }
}
