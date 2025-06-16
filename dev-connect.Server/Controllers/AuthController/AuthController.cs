using dev_connect.Server.Data.Dto;
using dev_connect.Server.Services.AuthService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dev_connect.Server.Controllers.AuthController
{
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authservice = authService;

        [HttpPost]
        [Route("/auth/login")]
        public IActionResult Login([FromQuery] string userName, [FromQuery] string password)
        {
            var response = _authservice.Login(userName, password);
            return Ok(response);
        }
        [HttpPost]
        [Route("/auth/register")]
        public IActionResult Register([FromBody] RegisterUserDto userDto)
        {
            _authservice.Register(userDto);
            return Ok();
        }
        [HttpPost]
        [Route("/auth/forgot-password")]
        public IActionResult ForgotPassword([FromQuery] string email)
        {
            _authservice.ForgotPassword(email);
            return Ok();
        }
    }
}
