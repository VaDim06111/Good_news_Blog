using Good_news_Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Repositories
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
