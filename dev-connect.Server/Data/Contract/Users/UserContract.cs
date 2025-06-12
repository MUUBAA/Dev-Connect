namespace dev_connect.Server.Data.Contract.Users
{
    public class UserContract
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Image { get; set; }

    }
}
