using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
