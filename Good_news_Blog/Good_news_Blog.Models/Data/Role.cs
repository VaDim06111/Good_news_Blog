using Good_news_Blog.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Models
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
