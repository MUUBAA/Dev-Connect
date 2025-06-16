namespace dev_connect.Server.Data.Contract.Users
{
    public class UserUpdate
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? UserName { get; set; }
        public string? Image { get; set; }
    }
}
