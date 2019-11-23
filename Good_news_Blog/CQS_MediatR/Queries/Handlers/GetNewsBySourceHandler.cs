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
    public class GetNewsBySourceHandler : IRequestHandler<GetNewsBySourceQuery, IEnumerable<News>>
    {
        private readonly ApplicationDbContext _context;

        public GetNewsBySourceHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetNewsBySourceQuery request, CancellationToken cancellationToken)
        {
            var result =  await _context.News.Where(s => s.Source.Equals(request.Source)).ToListAsync(cancellationToken);

            return result;
        }
    }
}
