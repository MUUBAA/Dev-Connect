using System.ComponentModel.DataAnnotations.Schema;
using dev_connect.Server.Data.Base;
using dev_connect.Server.Data.Enums;

namespace dev_connect.Server.Data.Entities.Users
{
    [Table("users")]
    public class User : BaseEntities
    {
        [Column("name")]
        public required string? Name { get; set; }
        [Column("email")]
        public required string? Email { get; set; }
        [Column("username")]
        public required string? UserName { get; set; }
        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("isverification_sent")]
        public bool IsVerificationSent { get; set; }
        [Column("email_verefied_at")]
        public DateTime? EmailVerefiedAt { get; set; }
        [Column("status")]
        public VisibleStatus Status { get; set; }
        [Column("is_email_verified")]
        public bool IsEmailVerified { get; set; }
        [Column("invitation_token")]
        public string? InvitationToken { get; set; }
        [Column("invitation_send_at")]
        public DateTime? InvitationSendAt { get; set; }
        [Column("invitation_token_expiry")]
        public DateTime? InvitationTokenExpiry { get; set; }
    }
}
