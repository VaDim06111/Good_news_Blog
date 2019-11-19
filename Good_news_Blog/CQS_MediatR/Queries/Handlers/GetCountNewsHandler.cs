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
    public class GetCountNewsHandler : IRequestHandler<GetCountNewsQuery, long>
    {
        private readonly ApplicationDbContext _context;

        public GetCountNewsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(GetCountNewsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.LongCountAsync(cancellationToken);

            return result;
        }
    }
}
