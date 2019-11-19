using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Queries.QuerieEntities
{
    public class GetNewsByIdQuery : IRequest<News>
    {
        public Guid Id { get; }

        public GetNewsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
