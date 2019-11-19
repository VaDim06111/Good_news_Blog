using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Commands.CommandEntities
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteCommentCommand(Guid id)
        {
            Id = id;
        }
    }
}
