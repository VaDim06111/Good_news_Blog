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
    public class GetAllNewsHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<News>>
    {
        private ApplicationDbContext _context;

        public GetAllNewsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.ToListAsync(cancellationToken);

            return result;
        }
    }
}
