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
    public class AddNewsHandler : IRequestHandler<AddNewsCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public AddNewsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            await _context.News.AddAsync(request.News, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
