using Core;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserNewsFromOnliner
{
    public class NewsParserFromOnliner : INewsOnlinerParser
    {        
        private readonly IUnitOfWork _unitOfWork;
        private const string urlOnliner = @"https://people.onliner.by/";
        List<string> links = new List<string>();

        public NewsParserFromOnliner(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<News> GetFromUrl()
        {           
            var web = new HtmlWeb();
            var doc = web.Load(urlOnliner);

            //var nodes = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div");
            var nodes = doc.DocumentNode.SelectNodes("//html/body");

            foreach (var node in nodes)
            {
                GetLink(node.SelectNodes("//div"));
            }           

            return null;
        }

        public bool Add(News news)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(News news)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(IEnumerable<News> objects)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(IEnumerable<News> objects)
        {
            throw new NotImplementedException();
        }

        public void GetLink(HtmlNodeCollection nodes)
        {
            foreach (var div in nodes)
            {
                var node = div.SelectNodes("//a[@class='news-tiles__stub']");

                foreach (var item in node)
                {
                    if (!links.Contains(item.Attributes["href"].Value))
                    {
                        links.Add(item.Attributes["href"].Value);
                    }
                }
            }            
        }     
    }
}
