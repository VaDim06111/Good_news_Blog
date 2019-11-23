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
    public class ChangeUserRolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ChangeUserRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// GET api/changeuserroles
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
        /// GET api/changeuserroles
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
                    //===== получаем пользователя ======
                    var user = await _userManager.FindByIdAsync(userId);

                    //====== получем список ролей пользователя ======
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();

                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };

                    return Ok(model);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// POST api/changeuserroles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns>Ok()</returns>
        [HttpPost]
        public async Task<ActionResult> Post(string userId, [FromBody] List<string> roles)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    //===== получаем пользователя =====
                    var user = await _userManager.FindByIdAsync(userId);
                    //===== получем список ролей пользователя =====
                    var userRoles = await _userManager.GetRolesAsync(user);
                    //===== получаем все роли =====
                    var allRoles = _roleManager.Roles.ToList();
                    //===== получаем список ролей, которые были добавлены =====
                    var addedRoles = roles.Except(userRoles);
                    //===== получаем роли, которые были удалены =====
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);
                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server error");
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// PUT api/changeuserroles
        /// </summary>
        /// <param name="userId"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] string userId)
        {
        }

        /// <summary>
        /// DELETE api/changeuserroles
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}