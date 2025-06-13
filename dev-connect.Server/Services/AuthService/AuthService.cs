using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;
using dev_connect.Server.Services.UserService;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;

namespace dev_connect.Server.Services.AuthService
{
    public interface IAuthService
    {
        string Login(string userName, string password);
    }
    public class AuthService(IConfiguration configuration, IUserService userService) : IAuthService
    {
        private readonly string _JWTSecret = configuration.GetValue("JWTSecret", string.Empty)!;
        private readonly IUserService _userService = userService;


        public string Login(string userName, string password)
        {
            var user = _userService.GetUserByUserName(userName) ?? throw new Exception("User not found");
            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid password");
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
    }
}
