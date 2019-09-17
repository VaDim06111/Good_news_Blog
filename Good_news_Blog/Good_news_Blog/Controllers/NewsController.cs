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
        public async Task<IActionResult> SaveNews(News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                await db.SaveChangesAsync();

                return RedirectToAction("NewsAdded", "News");
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}