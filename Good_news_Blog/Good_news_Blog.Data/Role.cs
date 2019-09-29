using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
