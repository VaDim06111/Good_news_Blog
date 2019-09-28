using Good_news_Blog.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Models
{
    public class News : BaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public DateTime DatePublication { get; set; }
        [Required]
        public byte IndexOfPositive { get; set; }

    }
}
