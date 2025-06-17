using dev_connect.Server.Data.Contract.Users;
using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;
using dev_connect.Server.Services.UserService;
using dev_connect.Server.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dev_connect.Server.Controllers.UserController
{
    [Authorize]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [Route("/user/create")]

        public ActionResult<GenericApiResponse<string>> CreateUser([FromBody] UserContract request)
        {
            var response = _userService.CreateUser(request);
            return Ok(new GenericApiResponse<string>(true, response ? "User Create Successfully" : "Failed to Create User"));

        }
        [HttpPut]
        [Route("/user/update")]
        public ActionResult<GenericApiResponse<string>> UpdateUser([FromBody] UserUpdate contract)
        {
            var response = _userService.UpdateUser(contract);
            return Ok(new GenericApiResponse<string>(true, response ? "User updated sucessfully" : "Failed to update user"));
        }

        [HttpGet]
        [Route("/user/getById")]
        public ActionResult GetUser([FromQuery] int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(new GenericApiResponse<User>(true, "Sucess", user));
        }

        [HttpGet]
        [Route("/user/getUserByUserName")]
        public ActionResult<GenericApiResponse<UserDto>> GetUserByUserName([FromQuery] string username)
        {
            var user = _userService.GetUserByUserName(username);
            return Ok(new GenericApiResponse<UserDto>(true, "success", user));
        }
        [HttpGet]
        [Route("/user/getUserByEmail")]
        public ActionResult<GenericApiResponse<UserDto>> GetUserByEmail([FromQuery] string email)
        {
            var user = _userService.GetUserByEmail(email);
            return Ok(new GenericApiResponse<UserDto>(true, "Sucess", user));
        }
    }
}
