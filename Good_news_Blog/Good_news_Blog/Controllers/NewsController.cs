using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Good_news_Blog.Controllers
{
    public class NewsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public NewsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }

        public IActionResult NewsAdded()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(News news)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.News.AddAsync(news);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("NewsAdded", "News");
            }
            else
                return RedirectToAction("Error", "Home");
        }
    }
}