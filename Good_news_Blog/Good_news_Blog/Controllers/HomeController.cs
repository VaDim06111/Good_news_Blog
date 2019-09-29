using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Models;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Good_news_Blog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;        
        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IActionResult Index()
        {
            //return View(db.News.ToList());           
            return View(_unitOfWork.News.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ReadMore(Guid id)
        {            
            var news = _unitOfWork.News.Where(p => p.NewsId.Equals(id)).FirstOrDefault();           
            return View(news);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
