using Good_news_Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Good_news_Blog.Repositories
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
