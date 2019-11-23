using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Good_news_Blog.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/users
        /// </summary>
        /// <returns>Ok(await _userManager.Users.ToListAsync())</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _userManager.Users.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// GET api/users
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok(_userManager.Users.Where(s => s.Id.Equals(id)).ToString())</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                return Ok(_userManager.Users.Where(s => s.Id.Equals(id)).ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// POST api/users
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser { Email = model.Email, UserName = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    await _userManager.AddToRoleAsync(user, "user");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// PUT api/users
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        /// <summary>
        /// DELETE api/users
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok()</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var role = await _userManager.FindByIdAsync(id);
                    await _userManager.DeleteAsync(role);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}