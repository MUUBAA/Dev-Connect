namespace dev_connect.Server.Data.Contract.Users
{
    public class UserContract
    {
        public  string? Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Image { get; set; }
        public string? AuthProvider { get; set; }
    }
}
