using Good_news_Blog.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Models
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }      
        [Required]
        public string UserName { get; set; }       
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Compare("Email",ErrorMessage ="Email don't confirm!")]
        public string ConfirmEmail { get; set; }
        public string Phone { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }

    }
}
