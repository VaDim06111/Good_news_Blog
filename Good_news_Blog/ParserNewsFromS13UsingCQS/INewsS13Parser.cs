﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Good_news_Blog.Data;

namespace ParserNewsFromS13UsingCQS
{
    public interface INewsS13Parser : IParserCQS<News>
    {
    }
}
