using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CQS_MediatR.Commands.CommandEntities;
using CQS_MediatR.Queries.QuerieEntities;
using Good_news_Blog.Data;
using Good_news_Blog.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/comment
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

                Log.Information("Get news model by newsId was successfully");
                return Ok(newsModel);
            }
            catch (Exception ex)
            {
                Log.Error($"Get news model by newsId was fail with exception:{Environment.NewLine}{ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// POST api/comment
        /// </summary>
        /// <param name="text"></param>
        /// <param name="id"></param>
        /// <returns>Ok(comment)</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] string text, Guid id)
        {
            try
            {
                var author = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

                var comment = new Comment()
                {
                    Author = author,
                    Text = text,
                    PubDateTime = DateTime.SpecifyKind(
                        DateTime.UtcNow,
                        DateTimeKind.Utc),
                    CountDislikes = 0,
                    CountLikes = 0,
                    News = await _mediator.Send(new GetNewsByIdQuery(id))
                };

                await _mediator.Send(new AddCommentCommand(comment));

                Log.Information("Post new comment  was successfully");

                return StatusCode(201, comment);
            }
            catch (Exception ex)
            {
                Log.Error($"Post new comment was fail with exception:{Environment.NewLine}{ex.Message}");
                return BadRequest();
            }

        }

        /// <summary>
        /// PUT api/comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCommentCommand(id));
                Log.Information("Delete comment  was successfully");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Delete comment  was fail with exception: {ex.Message}");
                return true;
            }
        }
    }
}