namespace dev_connect.Server.Data.Dto
{
    public class JWTUserParam
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string DisplayName { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
