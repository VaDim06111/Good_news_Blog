using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Models;
using Good_news_Blog.Repositories;
using Good_news_Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Good_news_Blog.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(IUnitOfWork uow, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = uow;
            _userManager = userManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> SendComment([FromBody] CommentModel model)
        {
            var author = _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var comment = new Comment()
            {
                Author = author,
                Text = model.Text,
                PubDateTime = DateTime.SpecifyKind(
                    DateTime.UtcNow,
                    DateTimeKind.Utc),
                CountDislikes = 0,
                CountLikes = 0,
                News = _unitOfWork.News.Where(i => i.Id.Equals(model.Id)).FirstOrDefault()
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();

            return Json(comment);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromBody] Guid id)
        {
            _unitOfWork.Comments.Delete(id);
            await _unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> _GetComments(Guid id)
        {
            var news = _unitOfWork.News.Where(p => p.Id.Equals(id)).FirstOrDefault();
            var commentModel = await _unitOfWork.Comments.Include("Author").Include("News").ToListAsync();
            var comments = commentModel.Where(i => i.News.Id.Equals(id)).OrderByDescending(o => o.PubDateTime);

            var newsModel = new NewsViewModel()
            {
                News = news,
                Comments = comments
            };

            return PartialView("_GetComments", newsModel);
        }
    }
}