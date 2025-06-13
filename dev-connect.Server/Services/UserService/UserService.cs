using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Repositories;
using static dev_connect.Server.Data.Exceptions.DataExceptions;

namespace dev_connect.Server.Services.UserService
{
    public interface IUserService
    {
        UserDto GetUserByUserName(string userName);
    }
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        

        public UserDto GetUserByUserName(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName) ?? throw new NotFoundException("User not found");
            return user;
        }
    }
}
