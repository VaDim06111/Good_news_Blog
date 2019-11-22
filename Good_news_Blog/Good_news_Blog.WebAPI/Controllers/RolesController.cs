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
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET api/roles
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _roleManager.Roles.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/roles/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {

            try
            {
                return Ok(_roleManager.Roles.Where(s => s.Id.Equals(id)).ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/roles
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    await _roleManager.CreateAsync(new IdentityRole(value));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }

                return Ok();
            }

            return BadRequest();
        }

        // PUT api/roles/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        // DELETE api/roles/e2abcf9b-f692-4630-9799-08d76e9f8705
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    await _roleManager.DeleteAsync(role);
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