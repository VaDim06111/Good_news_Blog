using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Good_news_Blog.Models;
using Good_news_Blog.Repositories;
using Good_news_Blog.Data;

namespace Good_news_Blog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
                
        public async Task<IActionResult> Index(int id = 1)
        {           
            IEnumerable<News> news = await _unitOfWork.News.ToListAsync();

            news = news.OrderByDescending(s => s.DatePublication.Date.ToString()).
                ThenByDescending(w=>w.DatePublication.TimeOfDay.ToString());

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
        public IActionResult ReadMore(Guid id)
        {
            var news = _unitOfWork.News.Where(p => p.Id.Equals(id)).FirstOrDefault();
            return View(news);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
