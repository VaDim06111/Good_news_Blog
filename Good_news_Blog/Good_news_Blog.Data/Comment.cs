using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Good_news_Blog.Data
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime PubDateTime { get; set; }
        public double Rating { get; set; }
        [Required]
        public IdentityUser Author { get; set; }
        [Required]
        public News News { get; set; }
    }
}
