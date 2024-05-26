using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Service.Application.Dto.Auth;
using Payment.Service.Application.Dto.User;
using Payment.Service.Application.Services.Auth;
using Payment.Service.Application.Services.User;

namespace Payment.Service.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserManager _userManager;

        public AccountController(IAuthService authService, IUserManager userManager)   
        {
            _authService= authService;
            _userManager= userManager;
        }

        [HttpPost("login")]
        public IActionResult token(LoginDto loginDto)
        {
            return Ok(_authService.Login(loginDto));
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult logout()
        {
            _authService.Logout(Request.Headers.Authorization);
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult create(UserInfoDto userInfoDto)
        {
            if (ModelState.IsValid)
            {
                _userManager.Create(userInfoDto);
                return Ok();
            }
            else
                return BadRequest();            
        }
    }
}