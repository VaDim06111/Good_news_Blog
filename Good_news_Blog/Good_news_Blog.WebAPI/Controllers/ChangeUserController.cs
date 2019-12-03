using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChangeUserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ChangeUserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/changeuser
        /// </summary>
        /// <returns>Ok(await _userManager.Users.ToListAsync())</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                Log.Information("Get all users was successfully");
                return Ok(await _userManager.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                Log.Error($"Get all users was fail with exception:{Environment.NewLine}{ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// GET api/changeuser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Ok(model)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string userId)
        {

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    EditUserViewModel model = new EditUserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    Log.Information("Get edit user model by userId was successfully");
                    return Ok(model);

                }
                catch (Exception ex)
                {
                    Log.Error($"Get edit user model by userId was fail with exception:{Environment.NewLine}{ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// POST api/changeuser
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EditUserViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.Id);

                    user.Email = model.Email;
                    user.UserName = model.Email;

                    await _userManager.UpdateAsync(user);

                    Log.Information("Update user information was successfully");
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Error($"Update user information was fail with exception:{Environment.NewLine}{ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// PUT api/changeuser
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        /// <summary>
        /// DELETE api/changeuser
        /// </summary>
        /// <param name="userId"></param>
        [HttpDelete("{id}")]
        public void Delete(string userId)
        {
        }
    }
}