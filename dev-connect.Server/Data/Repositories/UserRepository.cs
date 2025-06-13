using dev_connect.Server.Data.Dto;

namespace dev_connect.Server.Data.Repositories
{
    public interface IUserRepository
    {
        UserDto? GetUserByUserName(string userName);
    }
    public class UserRepository(Repository repository) : IUserRepository
    {
        public UserDto? GetUserByUserName(string userName)
        {
            var user = repository.Users
                .Where(u => u.UserName == userName)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    UserName = u.UserName,
                    EmailVerefiedAt = u.EmailVerefiedAt,
                    Image = u.Image,
                    PasswordHash = u.PasswordHash,
                    Createdt = u.CreatedAt,
                    CreatedBy = u.CreatedBy

                })
                .FirstOrDefault();

            return user;
        }
    }
}
