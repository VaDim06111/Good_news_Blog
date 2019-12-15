using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Core;
using CQS_MediatR.Commands.CommandEntities;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.Data;
using HtmlAgilityPack;
using MediatR;

namespace ParserNewsFromOnlinerUsingCQS
{
    public class NewsParserFromOnliner : INewsOnlinerParser
    {
        private readonly IMediator _mediator;
        private readonly ILemmatization _lemmatization;
        private const string UrlOnlinerRss = @"https://people.onliner.by/feed";

        public NewsParserFromOnliner(IMediator mediator, ILemmatization lemmatization)
        {
            _mediator = mediator;
            _lemmatization = lemmatization;
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
