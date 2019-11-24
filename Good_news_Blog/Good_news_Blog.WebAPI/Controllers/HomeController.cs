using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_MediatR.Queries.QuerieEntities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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

        /// <summary>
        /// GET api/home
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok(news)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id = 1)
        {
            int pageSize = 6;

            try
            {
                var news = await _mediator.Send(new GetNewsPageQuery(id, pageSize));
                news = news.OrderByDescending(s => s.DatePublication);

                Log.Information("Get news by id page was successfully");

                return Ok(news);
            }
            catch (Exception ex)
            {
                Log.Error($"Get news by id page was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// POST api/home
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// PUT api/home
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/home
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
