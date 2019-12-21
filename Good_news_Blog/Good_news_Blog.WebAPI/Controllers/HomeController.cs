using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.WebAPI.Models;
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
        /// <returns>Ok(model)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id = 1)
        {
            int pageSize = 12;

            try
            {
                var countNews = await _mediator.Send(new GetCountNewsQuery());
                if (countNews != 0)
                {
                    var countPages = (countNews % pageSize) == 0 ? countNews / pageSize : countNews / pageSize + 1;

                    var news = await _mediator.Send(new GetNewsPageQuery(id, pageSize));

                    NewsModel model = new NewsModel()
                    {
                        CountPages = countPages,
                        News = news
                    };

                    Log.Information("Get news by id page was successfully");

                    return Ok(model);
                }

                return NotFound();

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
