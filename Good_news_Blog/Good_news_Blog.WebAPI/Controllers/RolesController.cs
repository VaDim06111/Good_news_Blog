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

        /// <summary>
        /// GET api/roles
        /// </summary>
        /// <returns>Ok(await _roleManager.Roles.ToListAsync())</returns>
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

        /// <summary>
        /// GET api/roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok(_roleManager.Roles.Where(s => s.Id.Equals(id)).ToString())</returns>
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

        /// <summary>
        /// POST api/roles
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Ok()</returns>
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

        /// <summary>
        /// PUT api/roles
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        /// <summary>
        /// DELETE api/roles
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