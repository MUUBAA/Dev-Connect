namespace dev_connect.Server.Data.Dto
{
    public class ResetPassword
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
