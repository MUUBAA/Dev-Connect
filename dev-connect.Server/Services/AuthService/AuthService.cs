using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dev_connect.Server.Data.Contract.Users;
using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;
using dev_connect.Server.Services.MessageService;
using dev_connect.Server.Services.UserService;
using dev_connect.Server.Utils;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static dev_connect.Server.Data.Exceptions.DataExceptions;

namespace dev_connect.Server.Services.AuthService
{
    public interface IAuthService
    {
        string Login(string userName, string password);
        bool Register(RegisterUserDto userDto);
        bool ForgotPassword(string email);
    }
    public class AuthService(IConfiguration configuration, IUserService userService, IEmailService emailService, IEmailTemplate emailTemplate) : IAuthService
    {
        private readonly string _JWTSecret = configuration.GetValue("JWTSecret", string.Empty)!;

        private readonly IUserService _userService = userService;

        private readonly IEmailService _emailService = emailService;

        private readonly IEmailTemplate _emailTemplate = emailTemplate;

        private readonly IConfiguration _configuration = configuration;

        private readonly string _appUrl = configuration.GetValue<string>("AppUrl") ?? string.Empty;
        public string Login(string username, string password)
        {
            var user = _userService.GetUserByUserName(username) ?? throw new Exception("User not found");

            var passwordHasher = new PasswordHasher<User>();

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password");
            }
            if (user.IsEmailVerified == false)
            {
                throw new NotFoundException("User not found");
            }

            var generatedToken = GenerateToken(new JWTUserParam
            {
                Email = user.Email,
                DisplayName = user.Name,
                UserName = user.UserName,
                CreatedAt = user.Createdt
            }, 2);
            return generatedToken;
        }
        public bool Register(RegisterUserDto userDto)
        {
            var existingUser = _userService.GetUserByEmail(userDto.Email);
            if(existingUser != null)
            {
                throw new EntityDuplicateException("User already exists with this email");
            }
            var contract = new UserContract
            {   
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = new PasswordHasher<UserContract>().HashPassword(null, userDto.Password)
            };
            return _userService.CreateUser(contract);
        }
        private string GenerateToken(JWTUserParam param, int expiryInDays)
        {
            if (string.IsNullOrEmpty(_JWTSecret))
            {
                throw new ArgumentException("JWT Secret Key is Not Configured");
            }
            var token = JwtBuilder.Create()
                 .WithAlgorithm(new HMACSHA256Algorithm())
                 .WithSecret(_JWTSecret)
                 .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(expiryInDays).ToUnixTimeSeconds())
                 .AddClaim("email", param.Email)
                 .AddClaim("displayName", param.DisplayName)
                 .AddClaim("username", param.UserName)
                 .AddClaim("issuedAt", new DateTimeOffset(param.CreatedAt).ToUnixTimeSeconds())
                 .AddClaim(JwtRegisteredClaimNames.Jti, param.UserName)
                 .AddClaim(ClaimTypes.NameIdentifier, param.Email)
                 .Encode();

            return token;
        }
        public bool ForgotPassword(string email)
        {
            var user = _userService.GetUserByEmail(email) ?? throw new Exception("User not found");
            var resetToken = GeneratePasswordResetToken(user.Id);
            var resetLink = $"{_appUrl}/reset-password={resetToken}";

            return SendForgotPasswordEmail(user, resetLink);
        }

        private bool SendForgotPasswordEmail(UserDto user, string resetLink)
        {
            var (Subject, mailBody) = _emailTemplate.SendForgotPasswordEmail(user, resetLink);
            _emailService.SendEmail(user.Email, Subject, mailBody);
            return true;
        }
        public string GeneratePasswordResetToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("purpose", "password_reset")
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
