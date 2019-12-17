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
    public class GetCommentModelHandler : IRequestHandler<GetCommentModelQuery, IEnumerable<Comment>>
    {
        private readonly ApplicationDbContext _context;

        public GetCommentModelHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCommentModelQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Comments
                .Include(x => x.Author)
                .Where(n=> n.NewsId.Equals(request.Id))
                .OrderByDescending(o=>o.PubDateTime)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
