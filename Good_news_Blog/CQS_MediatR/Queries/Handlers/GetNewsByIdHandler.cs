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
    public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, News>
    {
        private readonly ApplicationDbContext _context;

        public GetNewsByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(s => s.Id.Equals(request.Id),cancellationToken);

            return result;
        }
    }
}
