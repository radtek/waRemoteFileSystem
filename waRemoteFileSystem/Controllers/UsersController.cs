﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using waRemoteFileSystem.Core;
using waRemoteFileSystem.Models;

namespace waRemoteFileSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Users")]
    public class UsersController : ControllerBase
    {
        private IUserService user; 

        public UsersController(IUserService _user)
        {
            user = _user;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [SwaggerOperation("New user")]
        public async Task<ActionResult> NewUser([FromBody] RegisterUserModel model)
        {
            var res = await user.RegisterUserAsync(model);
            return Ok(res);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation("User Authentication")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserRequest model)
        {
            var res = await user.AuthenticateAsync(model);
            return Ok(res);
        }
    }
}
