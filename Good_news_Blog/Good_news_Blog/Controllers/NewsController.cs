using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Good_news_Blog.Controllers
{
    public class NewsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
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
                _unitOfWork.News.Add(news);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("NewsAdded", "News");
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}