using dev_connect.Server.Data.Base;

namespace dev_connect.Server.Data.Entities.AuthToken
{
    public class AuthToken : BaseEntities
    {
        public string? UserName { get; set; }
        public string? JwtUniqueId { get; set; }
        public DateTime TokenExpiry { get; set; }
    }
}
