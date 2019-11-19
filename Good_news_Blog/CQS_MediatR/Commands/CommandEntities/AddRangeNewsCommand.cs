using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Commands.CommandEntities
{
    public class AddRangeNewsCommand : IRequest<Guid[]>
    {
        public IEnumerable<News> News { get; }

        public AddRangeNewsCommand(IEnumerable<News> news)
        {
            News = news;
        }
    }
}
