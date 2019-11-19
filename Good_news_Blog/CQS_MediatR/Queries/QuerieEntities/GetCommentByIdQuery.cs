using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Queries.QuerieEntities
{
    public class GetCommentByIdQuery : IRequest<Comment>
    {
        public Guid Id { get; }

        public GetCommentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
