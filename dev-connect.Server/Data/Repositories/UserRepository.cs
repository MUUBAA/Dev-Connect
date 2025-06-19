using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;
using static dev_connect.Server.Data.Exceptions.DataExceptions;

namespace dev_connect.Server.Data.Repositories
{
    public interface IUserRepository
    {
        int AddUser(User user);
        UserDto? GetUserByUserName(string userName);
        UserDto? GetUserByEmail(string email);
        User? GetUserById(int id);
        User? GetUserByInvitationToken(string token);
        void UpdateUser(User user);
    }
    public class UserRepository(Repository repository) : IUserRepository
    {
        public int AddUser(User user)
        {
            var existingUser = repository.Users.FirstOrDefault(u => u.Email == user.Email);
            if(existingUser != null)
            {
                if(!existingUser.IsDeleted)
                {
                    throw new EntityDuplicateException("user in this email already exisist");
                }
                else
                {
                    existingUser.Name = user.Name;
                    existingUser.UserName = user.UserName;
                    existingUser.PasswordHash = user.PasswordHash;
                    existingUser.Status = Enums.VisibleStatus.Active;
                    existingUser.IsDeleted = false;
                    existingUser.UpdatedAt = DateTime.UtcNow;
                    existingUser.UpdatedBy = user.UpdatedBy;
                    existingUser.InvitationToken = Guid.NewGuid().ToString();
                    existingUser.InvitationSendAt = DateTime.UtcNow;
                    repository.SaveChanges();
                    return existingUser.Id;

                }
            }
            repository.Add(user);
            repository.SaveChanges();
            return user.Id;
        }
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
        public UserDto GetUserByEmail(string email)
        {
            var user = repository.Users
                .Where(u => u.Email == email)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    UserName = u.UserName,
                    EmailVerefiedAt = u.EmailVerefiedAt,
                    Image = u.Image,
                    PasswordHash = u.PasswordHash,
                    Createdt = u.CreatedAt,
                    CreatedBy = u.CreatedBy,
                    Email = u.Email,

                })
                .FirstOrDefault();
            return user;
        }
        public User? GetUserById(int id)
        {
            return repository.Users.FirstOrDefault(u => u.Id == id);
        }
        public User? GetUserByInvitationToken(string token)
        {
            return repository.Users.FirstOrDefault(u => u.InvitationToken == token);
        }
        public void UpdateUser(User user)
        {
            var existingUser = repository.Users.FirstOrDefault(u => u.Email == user.Email) ?? throw new NotFoundException("User not found");
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.EmailVerefiedAt = user.EmailVerefiedAt;
            existingUser.InvitationToken = user.InvitationToken;
            existingUser.InvitationTokenExpiry = user.InvitationTokenExpiry;
            existingUser.UpdatedAt = DateTime.UtcNow;
            existingUser.UpdatedBy = user.UpdatedBy;
            repository.SaveChanges();
        }
    }
}
