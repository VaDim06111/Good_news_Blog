using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Good_news_Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class AdminController : Controller
    {
    }
}