using Core;
using Good_news_Blog.Data;
using Good_news_Blog.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ParserNewsFromOnliner
{
    public class NewsParserFromOnliner : INewsOnlinerParser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILemmatization _lemmatization;
        private const string UrlOnlinerRss = @"https://people.onliner.by/feed";

        //List<string> links = new List<string>() { @"https://people.onliner.by/feed", @"https://auto.onliner.by/feed" , @"https://tech.onliner.by/feed", @"https://realt.onliner.by/feed" };

        public NewsParserFromOnliner(IUnitOfWork uow, ILemmatization lemmatization)
        {
            _unitOfWork = uow;
            _lemmatization = lemmatization;
        }

        public IEnumerable<News> GetFromUrl()
        {
            List<News> news = new List<News>();

            XmlReader feedReader = XmlReader.Create(UrlOnlinerRss);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    var text = GetText(article.Links.FirstOrDefault().Uri.ToString());
                    news.Add(new News()
                    {
                        Title = article.Title.Text.Replace("&nbsp;", ""),
                        Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                        Source = article.Links.FirstOrDefault().Uri.ToString(),
                        DatePublication = article.PublishDate.UtcDateTime,
                        IndexOfPositive = _lemmatization.GetIndexOfPositive(text).Result,
                        Text = text
                    });
                }
            }


            return news;
        }

        public bool Add(News news)
        {
            if (_unitOfWork.News.Where(u => u.Source.Equals(news.Source)).Count() == 0)
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
                if (!_unitOfWork.News.Where(u => u.Source.Equals(item.Source)).Any() && !_unitOfWork.News.Where(u => u.Title.Contains(item.Title)).Any())
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
                if (!_unitOfWork.News.Where(u => u.Source.Equals(item.Source)).Any() && !_unitOfWork.News.Where(u=>u.Title.Contains(item.Title)).Any())
                {
                    await _unitOfWork.News.AddAsync(item);
                }
            }

            return true;
        }

        public string GetText(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            string text = "";

            var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/p");

            foreach (var item in node)
            {
                if (text == "")
                {
                    text = item.InnerText;
                }
                else
                {
                    text += Environment.NewLine + item.InnerText;
                }
            }

            text = text.Replace("  ", "");

            return text;
        }
    }
}
