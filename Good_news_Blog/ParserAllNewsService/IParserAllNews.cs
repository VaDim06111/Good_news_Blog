using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ParseNewsFromTutByUsingCQS;
using ParserNewsFromOnlinerUsingCQS;
using ParserNewsFromS13UsingCQS;

namespace ParserAllNewsService
{
    public interface IParserAllNews
    {
        Task<bool> ParseAllNews();
    }
}
