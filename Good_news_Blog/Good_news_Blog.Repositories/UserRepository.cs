using Good_news_Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Good_news_Blog.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
