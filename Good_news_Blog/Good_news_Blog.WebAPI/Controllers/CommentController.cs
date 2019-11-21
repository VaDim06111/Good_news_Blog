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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/comment/e2abcf9b-f692-4630-9799-08d76e9f8705
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

        // POST api/comment
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string text, Guid id)
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

            return Ok(comment);
        }

        // PUT api/comment/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/comment/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return (await _mediator.Send(new DeleteCommentCommand(id)));
        }
    }
}