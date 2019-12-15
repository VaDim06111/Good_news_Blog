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
using Good_news_Blog.Data;
using HtmlAgilityPack;
using MediatR;

namespace ParserNewsFromS13UsingCQS
{
    public class NewsParserFromS13 : INewsS13Parser
    {
        private readonly IMediator _mediator;
        private readonly ILemmatization _lemmatization;
        private const string urlS13 = @"http://s13.ru/rss";

        public NewsParserFromS13(IMediator mediator, ILemmatization lemmatization)
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

            if (node != null)
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
