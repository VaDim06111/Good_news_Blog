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
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCommentHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _context.Comments.Find(request.Id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
