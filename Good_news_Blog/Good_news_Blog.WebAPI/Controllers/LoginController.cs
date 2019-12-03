using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Good_news_Blog.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// GET api/login
        /// </summary>
        /// <returns>NotFound()</returns>
        [HttpGet]
        public  ActionResult Get()
        {
            return NotFound();
        }

        // GET api/login/5
        /// <summary>
        /// GET api/login
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NotFound()</returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return NotFound();
        }

        /// <summary>
        /// POST api/login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>GenerateJwtToken(model.Email, appUser)</returns>
        [HttpPost]
        public async Task<object> Post([FromBody] LoginModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    Log.Information("Login operation was successfully");
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    return GenerateJwtToken(model.Email, appUser);
                }

                Log.Information("Login operation was fail");
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Log.Error($"Login operation was fail with exception: {ex.Message}");
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// PUT api/login
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/login
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}