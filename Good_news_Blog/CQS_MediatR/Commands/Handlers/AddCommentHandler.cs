using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQS_MediatR.Commands.CommandEntities;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Commands.Handlers
{
    public class AddCommentHandler : IRequestHandler<AddCommentCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public AddCommentHandler(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<Guid> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            await _context.Comments.AddAsync(request.Comments, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Comments.Id;
        }
    }
}
