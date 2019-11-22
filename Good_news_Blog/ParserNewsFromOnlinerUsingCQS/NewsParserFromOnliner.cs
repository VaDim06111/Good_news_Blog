using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using CQS_MediatR.Commands.CommandEntities;
using Good_news_Blog.Data;
using HtmlAgilityPack;
using MediatR;

namespace ParserNewsFromOnlinerUsingCQS
{
    public class NewsParserFromOnliner : INewsOnlinerParser
    {
        private readonly IMediator _mediator;
        private const string UrlOnlinerRss = @"https://people.onliner.by/feed";

        public NewsParserFromOnliner(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<News> objects)
        {
            await _mediator.Send(new AddRangeNewsCommand(objects));

            return true;
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
                    news.Add(new News()
                    {
                        Title = article.Title.Text.Replace("&nbsp;", ""),
                        Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                        Source = article.Links.FirstOrDefault().Uri.ToString(),
                        DatePublication = article.PublishDate.UtcDateTime,
                        IndexOfPositive = 0,
                        Text = GetText(article.Links.FirstOrDefault().Uri.ToString())
                    });
                }
            }

            return news;
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
            text = text.Replace("\r\n", string.Empty);

            return text;
        }
    }
}
