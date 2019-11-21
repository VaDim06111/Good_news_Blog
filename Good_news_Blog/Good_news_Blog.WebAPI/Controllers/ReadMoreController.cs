using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadMoreController : Controller
    {
        private readonly IMediator _mediator;

        public ReadMoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/readmore/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var news = await _mediator.Send(new GetNewsByIdQuery(id));
                var comments = await _mediator.Send(new GetCommentModelQuery(id));
                comments = comments.OrderByDescending(o => o.PubDateTime);

                var newsModel = new NewsViewModel()
                {
                    News = news,
                    Comments = comments
                };

                return Ok(newsModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        // POST api/readmore
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/readmore/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/readmore/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}