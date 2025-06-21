using System.Net.Mail;

namespace dev_connect.Server.Services.MessageService
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        private string _SmtpHost;
        private int _SmtpPort;
        private string _SmtpMailAddress;
        private string _SmtpUserName;
        private string _SmtpPassword;
        private SmtpClient _client;

        private const string mailHeader = "<div style='background-color:transparent;font-family: system-ui;'><div class='m_-7955790069770508387block-grid' style='min-width:320px;max-width:600px;word-wrap:break-word;word-break:break-word;Margin:0 auto;background-color:#ffffff'><div style='border-collapse:collapse;display:table;width:100%;background-color:#ffffff'><div class='m_-7955790069770508387col' style='min-width:320px;max-width:600px;display:table-cell;vertical-align:top;width:600px'><div class='m_-7955790069770508387col_cont' style='width:100%!important'><div style='border-top:0px solid #000000;border-left:0px solid #000000;border-bottom:0px solid #000000;border-right:0px solid #000000;padding:0;'><table class='body-wrap' style='box-sizing: border-box; font-size: 14px; width: 100%; background-color: transparent; margin: 0;' bgcolor='transparent'><tr><td class='container' width='600' style='display: block !important; max-width: 600px !important; clear: both !important;' valign='top'><div class='content' style='padding: 20px;'><table class='main' width='100%' cellpadding='0' cellspacing='0' style='border: 1px solid rgba(130, 134, 156, 0.15);' bgcolor='transparent'><tr><td class='alert alert-primary border-0 bg-primary' style='padding: 20px; border-radius: 0; background:#E3EDF1; font-size: 21px; font-weight: 700;' align='center' valign='top'>Dev-Connect</td></tr><tr><td class='alert alert-dark border-0' style='padding: 20px; border-radius: 0;' align='center' valign='top'><p style='font-size:21px;color:#368EA8'><b>{Subject}</b></p></td></tr><tr><td style='padding: 5px'></td></tr></table></div></td></tr></table></div></div></div></div></div></div>";
        private const string mailFooter = "<tr> <td class='content-block' style='font-size: 14px; padding: 10px;background-color:#449ad4;color:#ffffff' valign='top'><p style='text-align: center;'><b>Dev-Connect</td> </tr> </table> </td> </tr> </table> </div> </td> <td style='box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;' valign='top'></td> </tr> </table> </div> </div> </div> </div> </div> </div> </div>";

        public EmailService(IConfiguration configuration)
        {
            _SmtpHost = configuration["SMTP:Host"];
            _SmtpPort = int.Parse(configuration["SMTP:Port"]);
            _SmtpMailAddress = configuration["SMTP:MailAddress"];
            _SmtpPassword = configuration["SMTP:Password"];
            _SmtpUserName = configuration["SMTP:UserName"];

            _client = new()
            {
                Port = _SmtpPort,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(_SmtpMailAddress, _SmtpPassword), // ***use valid credentials***;
                Host = _SmtpHost,
                EnableSsl = bool.Parse("true")
            };
        }
        public void SendEmail(string toAddress, string subject, string mailBody)
        {
            try
            {
                MailAddress fromAddress = new(_SmtpMailAddress, _SmtpUserName);
                MailMessage mail = new();
                mail.From = fromAddress;
                foreach (var address in toAddress.Split([","], StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(address);
                }
                var header = mailHeader.Replace("{Subject}", subject);
                string message = header + mailBody + mailFooter;

                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = message;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                _client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Exception occured - {0}", ex.Message);
            }
        }
    }
}
