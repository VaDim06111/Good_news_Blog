using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsFromOnliner
{
    public class NewsParserFromOnliner : INewsOnlinerParser
    {        
        private readonly IUnitOfWork _unitOfWork;
        private const string urlOnliner = @"https://www.onliner.by";

        public NewsParserFromOnliner(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<News> GetFromUrl(string url)
        {
            

            var web = new HtmlWeb();
            var doc = web.Load(url);

            var node = doc.GetElementbyId("container");          
            return null;
        }

        public News GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<News> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Add(News news)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(News news)
        {
            throw new NotImplementedException();
        }
    }
}
