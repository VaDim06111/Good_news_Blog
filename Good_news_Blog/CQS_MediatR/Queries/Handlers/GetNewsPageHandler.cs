using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQS_MediatR.Queries.Handlers
{
    public class GetNewsPageHandler : IRequestHandler<GetNewsPageQuery, IEnumerable<News>>
    {
        private readonly ApplicationDbContext _context;

        public GetNewsPageHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetNewsPageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.OrderByDescending(r => r.DatePublication)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
