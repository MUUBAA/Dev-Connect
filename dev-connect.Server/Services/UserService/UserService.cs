using System.Security.Cryptography;
using dev_connect.Server.Data.Contract.Users;
using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;
using dev_connect.Server.Data.Enums;
using dev_connect.Server.Data.Repositories;
using dev_connect.Server.Services.MessageService;
using dev_connect.Server.Utils;
using Microsoft.AspNetCore.Identity;
using static dev_connect.Server.Data.Exceptions.DataExceptions;

namespace dev_connect.Server.Services.UserService
{
    public interface IUserService
    {
        bool CreateUser(UserContract userContract);
        bool UpdateUser(UserUpdate user);
        UserDto GetUserByUserName(string userName);
        UserDto GetUserByEmail(string email);
        User GetUserById(int id);
        bool UpdateVerificationSentAt(int id);
    }
    public class UserService(IUserRepository userRepository, IUserContext userContext, IEmailService emailService, IEmailTemplate emailTemplate) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        private readonly IUserContext _userContext = userContext;
        private readonly IEmailService _emailService = emailService;
        private readonly IEmailTemplate _emailTemplate = emailTemplate;

        
        public bool CreateUser(UserContract contract)
        {
            var user = new User
            {
                Name = contract.Name,
                UserName = contract.UserName,
                Email = contract.Email,
                PasswordHash = new PasswordHasher<UserContract>().HashPassword(null, contract.Password),
                Status = VisibleStatus.InActive,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _userContext.UserId,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = _userContext.UserId
            };
            user.Id = _userRepository.AddUser(user);
            return true;
        }
        private bool SendInvitationEmail(User user)
        {
            var (subject, mailBody) = _emailTemplate.SendInvitationEmail(user);
            _emailService.SendEmail(user.Email, subject, mailBody);
            return UpdateVerificationSentAt(user.Id);
        }
        public bool UpdateUser(UserUpdate contract)
        {
            var user = new User
            {
                Id = contract.Id,
                Name = contract.Name,
                Email = contract.Email,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = _userContext.UserId,
                UserName = contract.UserName
            };
            _userRepository.UpdateUser(user);
            return true;
        }
        public UserDto GetUserByUserName(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName) ?? throw new NotFoundException("User not found");
            return user;
        }
        public UserDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email) ?? throw new NotFoundException("User not found");
            return user;
        }
        public User GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
        public bool UpdateVerificationSentAt(int id)
        {
            var user = _userRepository.GetUserById(id) ?? throw new NotFoundException("User not found");
            user.EmailVerefiedAt = DateTime.UtcNow;
            user.Status = VisibleStatus.Active;
            _userRepository.UpdateUser(user);
            return true;
        }
    }
}
