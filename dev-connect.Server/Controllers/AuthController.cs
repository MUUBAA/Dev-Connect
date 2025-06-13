using dev_connect.Server.Services.AuthService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dev_connect.Server.Controllers
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
    }
}
