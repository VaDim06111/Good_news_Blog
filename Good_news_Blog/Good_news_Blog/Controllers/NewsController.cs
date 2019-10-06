using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using ParserNewsFromOnliner;
using ParserNewsFromS13;

namespace Good_news_Blog.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsOnlinerParser _newsOnlinerParser;
        private readonly INewsS13Parser _newsS13Parser;
        
        
        public NewsController(IUnitOfWork uow, INewsOnlinerParser nop, INewsS13Parser nsp)
        {
            _unitOfWork = uow;
            _newsOnlinerParser = nop;
            _newsS13Parser = nsp;
        }

        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            var news = _newsS13Parser.GetFromUrl();
            await _newsS13Parser.AddRangeAsync(news);

            // var newsOnliner = _newsOnlinerParser.GetFromUrl();

            await _unitOfWork.SaveAsync();


            return RedirectToAction("NewsAdded", "News");
        }

        public IActionResult NewsAdded()
        {
            return View();
        }
      
    }
}