using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Good_news_Blog.Data;
using Good_news_Blog.Models;
using Good_news_Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> DeleteComment(Guid id, Guid comment)
        {
            _unitOfWork.Comments.Delete(id);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("ReadMore", "Home", new { id = comment });
        }
    }
}