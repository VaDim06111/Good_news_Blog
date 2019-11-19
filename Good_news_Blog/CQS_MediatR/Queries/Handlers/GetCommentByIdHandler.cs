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
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, Comment>
    {
        private readonly ApplicationDbContext _context;

        public GetCommentByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken);

            return result;
        }
    }
}
