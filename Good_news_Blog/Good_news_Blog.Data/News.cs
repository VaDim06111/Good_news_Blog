using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class News : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
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
