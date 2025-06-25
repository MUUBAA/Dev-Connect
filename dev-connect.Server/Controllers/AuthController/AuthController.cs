using dev_connect.Server.Data.Contract.Users;
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
        public ActionResult<GenericApiResponse<string>> CreateUser([FromBody] UserContract request)
        {
            var response = _userService.CreateUser(request);
            return Ok(new GenericApiResponse<string>(true, response ? "User Create Successfully" : "Failed to Create User"));

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
        [HttpPost]
        [Route("/auth/reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPassword request)
        {
            try
            {
                var result = _authservice.ResetPassword(request.Token, request.NewPassword);
                return Ok(new GenericApiResponse<string>(true, "Password reset successful"));
            }
            catch(Exception ex)
            {
                return BadRequest(new GenericApiResponse<string>(false, ex.Message));
            }
        }
    }
}
