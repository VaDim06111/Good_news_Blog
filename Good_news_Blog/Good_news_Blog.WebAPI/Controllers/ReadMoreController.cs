using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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

        /// <summary>
        /// GET api/readmore
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok(newsModel)</returns>
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

                Log.Information("Get newsModel was successfully");
                return Ok(newsModel);
            }
            catch (Exception ex)
            {
                Log.Error($"Get newsModel was fail with exception: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
        }

        /// <summary>
        /// POST api/readmore
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// PUT api/readmore
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/readmore
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}