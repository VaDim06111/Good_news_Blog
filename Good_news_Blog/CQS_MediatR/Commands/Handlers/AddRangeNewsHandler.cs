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
    public class AddRangeNewsHandler : IRequestHandler<AddRangeNewsCommand, Guid[]>
    {
        private readonly ApplicationDbContext _context;

        public AddRangeNewsHandler(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<Guid[]> Handle(AddRangeNewsCommand request, CancellationToken cancellationToken)
        {
            await _context.News.AddRangeAsync(request.News, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return  request.News.Select(s=>s.Id).ToArray();
        }
    }
}
