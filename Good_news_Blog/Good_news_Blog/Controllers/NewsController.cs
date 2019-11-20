using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParserNewsFromOnliner;
using ParserNewsFromS13;
using ParserNewsFromTutBy;

namespace Good_news_Blog.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsOnlinerParser _newsOnlinerParser;
        private readonly INewsS13Parser _newsS13Parser;
        private readonly INewsParserFromTutBy _newsTutByParser;


        public NewsController(IUnitOfWork uow, INewsOnlinerParser nop, INewsS13Parser nsp, INewsParserFromTutBy ntp)
        {
            _unitOfWork = uow;
            _newsOnlinerParser = nop;
            _newsS13Parser = nsp;
            _newsTutByParser = ntp;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            Parallel.Invoke(
                () =>
                {
                var newsS13 = _newsS13Parser.GetFromUrl();
                _newsS13Parser.AddRangeAsync(newsS13);
                }, 
                () =>
                {
                    var newsOnliner = _newsOnlinerParser.GetFromUrl();
                    _newsOnlinerParser.AddRangeAsync(newsOnliner);
                },
                () =>
                {
                    var newsTutBy = _newsTutByParser.GetFromUrl();
                    _newsOnlinerParser.AddRangeAsync(newsTutBy);
                });

            await _unitOfWork.SaveAsync();

            return RedirectToAction("NewsAdded", "News");
        }

        [Authorize(Roles = "admin")]
        public IActionResult NewsAdded()
        {
            return View();
        }

    }
}