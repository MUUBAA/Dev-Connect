using System.Security.Claims;

namespace dev_connect.Server.Utils
{
    public interface IUserContext
    {
        string UserId { get; }
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public string UserId => httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}