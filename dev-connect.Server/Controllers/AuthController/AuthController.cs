using dev_connect.Server.Data.Dto;
using dev_connect.Server.Services.AuthService;
using dev_connect.Server.Services.UserService;
using dev_connect.Server.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static dev_connect.Server.Data.Exceptions.DataExceptions;

namespace dev_connect.Server.Controllers.AuthController
{
    [ApiController]
    public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
    {
        private readonly IAuthService _authservice = authService;
        private readonly IUserService _userService = userService;

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


        [HttpGet]
        [Route("/auth/verify-email")]
        public IActionResult VerifyEmail([FromQuery] string token)
        {
            try
            {
                _userService.VerifyEmailByToken(token);
                return Ok(new GenericApiResponse<string>(true, "Email sucessfully verified", null));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new GenericApiResponse<string>(false, ex.Message, null));
            }
        }


        [HttpPost]
        [Route("auth/resend-invitation")]
        public IActionResult ResendInvitation([FromQuery] string email)
        {
            _userService.ResentInvitation(email);
            return Ok(new { message = "Invitation email sent successfully" });
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
