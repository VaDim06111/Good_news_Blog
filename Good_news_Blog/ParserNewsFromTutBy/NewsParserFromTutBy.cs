using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Good_news_Blog.Data;
using System.ServiceModel.Syndication;
using Good_news_Blog.Repositories;

namespace ParserNewsFromTutBy
{
    public class NewsParserFromTutBy : INewsParserFromTutBy
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string UrlTutByrRss = @"https://news.tut.by/rss/all.rss";


        public NewsParserFromTutBy(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<News> GetFromUrl()
        {
            List<News> news = new List<News>();

            XmlReader feedReader = XmlReader.Create(UrlTutByrRss);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    var text = GetText(article.Links.FirstOrDefault().Uri.ToString());
                    if (!string.IsNullOrEmpty(text))
                    {
                        news.Add(new News()
                        {
                            Title = article.Title.Text.Replace("&nbsp;", ""),
                            Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                            Source = article.Links.FirstOrDefault().Uri.ToString(),
                            DatePublication = article.PublishDate.UtcDateTime,
                            IndexOfPositive = 0,
                            Text = text
                        });
                    }
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

        public string GetText(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            string text = "";

            //var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/div/div");
            var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/div/div/div/p");

            if (node !=null)
            {
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

                var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ", "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ", "&laquo;", "&raquo; ", "&raquo;", "&quot;" };

                foreach (var item in mas)
                {
                    text = text.Replace(item, " ");
                }

                text = text.Replace("  ", " ");
                text = text.Replace("\r\n", string.Empty);
            }

            return text;
        }
    }
}
