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
        [Required(ErrorMessage = "Please enter the title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the text")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Please enter the source")]
        public string Source { get; set; }
        [Required(ErrorMessage = "Please enter the publication date")]
        public DateTime DatePublication { get; set; }
        [Required(ErrorMessage = "Please enter the index of positive")]
        public byte IndexOfPositive { get; set; }

    }
}
