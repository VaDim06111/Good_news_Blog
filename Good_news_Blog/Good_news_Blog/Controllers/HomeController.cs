using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Good_news_Blog.Models;
using Good_news_Blog.Repositories;
using Good_news_Blog.Data;
using Good_news_Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Good_news_Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1)
        {
            IEnumerable<News> news = await _unitOfWork.News.ToListAsync();
            news = news.OrderByDescending(s => s.DatePublication);
            
            int pageSize = 12;
            var count = await _unitOfWork.News.CountAsync();
            var items = news.Skip((id - 1) * pageSize).Take(pageSize).ToList();

            NewsPageViewModel pageViewModel = new NewsPageViewModel(count, id, pageSize);
            IndexViewModel viewModel = new IndexViewModel()
            {
                News = items,
                PageViewModel = pageViewModel
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReadMore(Guid id)
        {
            var news = _unitOfWork.News.Where(p => p.Id.Equals(id)).FirstOrDefault();
            var commentModel = await _unitOfWork.Comments.Include("Author").Include("News").ToListAsync();
            var comments = commentModel.Where(i => i.News.Id.Equals(id)).OrderByDescending(o => o.PubDateTime);

            var newsModel = new NewsViewModel()
            {
                News = news,
                Comments = comments
            };

            return View(newsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
