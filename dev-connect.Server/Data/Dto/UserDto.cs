using dev_connect.Server.Data.Enums;

namespace dev_connect.Server.Data.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public  string? Name { get; set; }
        public required string? Email { get; set; }
        public required string? UserName { get; set; }
        public string? Image { get; set; }
        public required string? PasswordHash { get; set; }
        public DateTime? EmailVerefiedAt { get; set; }
        public VisibleStatus Status { get; set; }
        public DateTime Createdt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
