namespace dev_connect.Server.Data.Contract.Auth
{
    public class LoginContract
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
