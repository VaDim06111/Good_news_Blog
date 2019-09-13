using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class User
    {
        public Guid Id { get; set; }        
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int AccessFailedCount { get; set; }

        public List<Role> Roles { get; set; }

    }
}
