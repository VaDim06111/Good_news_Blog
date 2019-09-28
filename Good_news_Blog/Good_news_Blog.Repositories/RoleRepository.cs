using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Good_news_Blog.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
