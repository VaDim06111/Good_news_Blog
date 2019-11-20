using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Queries.QuerieEntities
{
    public class GetNewsPageQuery : IRequest<IEnumerable<News>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetNewsPageQuery(int pageNumber = 1, int pageSize = 6)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
