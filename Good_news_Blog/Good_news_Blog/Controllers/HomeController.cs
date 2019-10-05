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

        public async Task<IActionResult> Index(int page=1)
        {           
            int pageSize = 12;
            var count = await _unitOfWork.News.CountAsync();
            var items = _unitOfWork.News.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            NewsPageViewModel pageViewModel = new NewsPageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel()
            {
                News = items,
                PageViewModel = pageViewModel,
                TotalPages = pageViewModel.TotalPages              
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

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
