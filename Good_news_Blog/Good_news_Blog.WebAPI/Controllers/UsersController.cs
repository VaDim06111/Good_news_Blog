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

        // GET api/users
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

        // GET api/users/e2abcf9b-f692-4630-9799-08d76e9f8705
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

        // POST api/users
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

        // PUT api/users/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        // DELETE api/users/e2abcf9b-f692-4630-9799-08d76e9f8705
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