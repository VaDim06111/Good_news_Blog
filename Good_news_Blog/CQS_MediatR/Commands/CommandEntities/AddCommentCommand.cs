using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using MediatR;

namespace CQS_MediatR.Commands.CommandEntities
{
    public class AddCommentCommand : IRequest<Guid>
    {
        public Comment Comments { get; }

        public AddCommentCommand(Comment comments)
        {
            Comments = comments;
        }
    }
}
