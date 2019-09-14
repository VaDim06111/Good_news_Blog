using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Good_news_Blog.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db;
        public NewsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult AddNews()
        {
            return View();
        }

        public IActionResult NewsAdded()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> SaveNews()
        {            
            string title = Request.Form.FirstOrDefault(p => p.Key == "title").Value;
            string text = Request.Form.FirstOrDefault(p => p.Key == "text").Value;
            string source = Request.Form.FirstOrDefault(p => p.Key == "source").Value;
            var date = Request.Form.FirstOrDefault(p => p.Key == "date").Value;
            string index = Request.Form.FirstOrDefault(p => p.Key == "index").Value;

            var news = new News()
            {
                Title = title,
                Text = text,
                Source = source,
                Date = Convert.ToDateTime(date),
                IndexOfPositive = Convert.ToByte(index)
            };

            db.News.Add(news);
            await db.SaveChangesAsync();

            return RedirectToAction("NewsAdded", "News");
        }
    }
}