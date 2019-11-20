using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_MediatR.Queries.Handlers;
using CQS_MediatR.Queries.QuerieEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/home/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id = 1)
        {
            int pageSize = 6;

            try
            {
                var news = await _mediator.Send(new GetNewsPageQuery(id, pageSize));
                news = news.OrderByDescending(s => s.DatePublication);
                return Ok(Json(news));
            }
            catch (NullReferenceException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/home
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/home/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
