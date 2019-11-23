using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Commands.CommandEntities
{
    public class AddNewsCommand : IRequest<bool>
    {
        public News News { get; }
        public AddNewsCommand(News news)
        {
            News = news;
        }
    }
}
