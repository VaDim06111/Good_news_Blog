using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQS_MediatR.Commands.CommandEntities;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.Data;
using MediatR;
using ParseNewsFromTutByUsingCQS;
using ParserNewsFromOnlinerUsingCQS;
using ParserNewsFromS13UsingCQS;

namespace ParserAllNewsService
{
    public class ParserAllNews : IParserAllNews
    {
        private readonly IMediator _mediator;
        private readonly INewsOnlinerParser _newsOnliner;
        private readonly INewsS13Parser _newsS13;
        private readonly INewsTutByParser _newsTutBy;

        public ParserAllNews(IMediator mediator, INewsOnlinerParser newsOnliner, INewsS13Parser newsS13, INewsTutByParser newsTutBy)
        {
            _mediator = mediator;
            _newsOnliner = newsOnliner;
            _newsS13 = newsS13;
            _newsTutBy = newsTutBy;
        }

        public async Task<bool> ParseAllNews()
        {
            IEnumerable<News> newsFromOnliner = new List<News>();
            IEnumerable<News> newsFromS13 = new List<News>();
            IEnumerable<News> newsFromTutBy = new List<News>();

            var allNews = await _mediator.Send(new GetAllNewsQuery());

            Parallel.Invoke(
                () =>
                {
                    newsFromS13 = _newsS13.GetFromUrl();

                    foreach (var news1 in newsFromS13)
                    {
                        if (allNews.Count(s => s.Source.Equals(news1.Source)) == 0)
                        {
                            _mediator.Send(new AddNewsCommand(news1));
                        }
                    }
                },
                () =>
                {
                    newsFromOnliner = _newsOnliner.GetFromUrl();
                    foreach (var news2 in newsFromOnliner)
                    {
                        if (allNews.Count(s => s.Source.Equals(news2.Source)) == 0)
                        {
                            _mediator.Send(new AddNewsCommand(news2));
                        }
                    }
                },
                () =>
                {
                    newsFromTutBy = _newsTutBy.GetFromUrl();
                    foreach (var news3 in newsFromTutBy)
                    {
                        if (allNews.Count(s => s.Source.Equals(news3.Source)) == 0)
                        {
                            _mediator.Send(new AddNewsCommand(news3));
                        }
                    }
                });

            return true;
        }
    }
}
