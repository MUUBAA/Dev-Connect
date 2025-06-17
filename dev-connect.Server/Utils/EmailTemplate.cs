using dev_connect.Server.Data.Dto;
using dev_connect.Server.Data.Entities.Users;

namespace dev_connect.Server.Utils
{
    public interface IEmailTemplate
    {
        (string subject, string body) SendInvitationEmail(User user);
        (string subject, string body) SendForgotPasswordEmail(UserDto user, string passwordHash);
    }
    public class EmailTemplate(IConfiguration configuration): IEmailTemplate
    {
        private readonly string _appUrl = configuration.GetValue<string>("AppUrl") ?? string.Empty;

        public (string subject, string body) SendInvitationEmail(User user)
        {
            var subject = "Invitation to join DEV-CONNECT";
            var mailBody = $"Dear {user.Name}, <br><br>" +
                           $"You have been invited to join DEV-CONNECT <br><br>" +
                           $"Please click on the following link to login: <a href={_appUrl}>Login</a>" +
                           $"Thank you, <br>" +
                           $"DEV-CONNECT Team";
            return (subject, mailBody);
        }

        public (string subject, string body) SendForgotPasswordEmail(UserDto user, string resetToken)
        {
            var subject = "Forgot Password";
            var mailBody = $"Dear {user.Name},<br><br>" +
                           $"You have requested to reset your password. Please use the following credentials to login:<br><br>" +
                           $"User Name: {user.Name}<br>" +
                           $"Please click on the following link to login: <a href='{_appUrl}/resetPassword/{resetToken}>Login</a>" +
                           $"Thank you, <br>" +
                           $"DEV-CONNECT Team";
            return (subject, mailBody);
        }
    }
}
