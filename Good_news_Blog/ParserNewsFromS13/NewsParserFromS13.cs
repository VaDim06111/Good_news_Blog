using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Core;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using HtmlAgilityPack;

namespace ParserNewsFromS13
{
    public class NewsParserFromS13 : INewsS13Parser
    {      
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILemmatization _lemmatization;
        private const string urlS13 = @"http://s13.ru/rss";

        public NewsParserFromS13(IUnitOfWork uow, ILemmatization lemmatization)
        {
            _unitOfWork = uow;
            _lemmatization = lemmatization;
        }

        public bool Add(News news)
        {
            if ( _unitOfWork.News.Where(u => u.Source.Equals(news.Source)).Count() == 0)
            {
                _unitOfWork.News.Add(news);

                return true;
            }
            _unitOfWork.News.Add(news);

            return true;
        }

        public async Task<bool> AddAsync(News news)
        {
            if (_unitOfWork.News.Where(u => u.Source.Equals(news.Source)).Count() == 0)
            {
                _unitOfWork.News.Add(news);

                return true;
            }
            await _unitOfWork.News.AddAsync(news);

            return true;
        }

        public bool AddRange(IEnumerable<News> news)
        {
            foreach (var item in news)
            {
                if (_unitOfWork.News.Where(u => u.Source.Equals(item.Source)).Count() == 0)
                {
                    _unitOfWork.News.Add(item);
                   
                }
               
            }
           
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<News> news)
        {
            foreach (var item in news)
            {                
                if (_unitOfWork.News.Where(u => u.Source.Equals(item.Source)).Count() == 0)
                {
                    await _unitOfWork.News.AddAsync(item);
                }
            }
            
            return true;
        }

        public IEnumerable<News> GetFromUrl()
        {
            XmlReader feedReader = XmlReader.Create(urlS13);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);

            List<News> news = new List<News>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    var text = GetTextOfNews(article.Links.FirstOrDefault().Uri.ToString());
                    if (!string.IsNullOrEmpty(text))
                    {
                        news.Add(new News()
                        {
                            Title = article.Title.Text,
                            Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                            Source = article.Links.FirstOrDefault().Uri.ToString(),
                            DatePublication = article.PublishDate.UtcDateTime,
                            IndexOfPositive = _lemmatization.GetIndexOfPositive(text).Result,
                            Text = text
                        });
                    }
                }
            }

            return news;
        }      

        public string GetTextOfNews(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/ul/li/div/div");

            if (node!=null)
            {
                var text = node.Skip(1).Take(1).FirstOrDefault().InnerText;
                var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ", "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ", "&laquo;", "&raquo; ", "&raquo;", "&quot;" };

                foreach (var item in mas)
                {
                    text = text.Replace(item, "");
                }

                Regex.Replace(text, "<.*?>", string.Empty);

                return text;
            }
            
            return "";
        }
        
      
    }
}
