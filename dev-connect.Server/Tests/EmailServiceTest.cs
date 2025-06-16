using dev_connect.Server.Services.MessageService;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System.Net.Mail;

namespace dev_connect.Server.Tests
{
    public class EmailServiceTest
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<SmtpClient> _smtpClientMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _smtpClientMock = new Mock<SmtpClient>();
        }

        [Test]
        public void Send_EmailSuccessfully()
        {
            // Arrange
            _configurationMock.SetupGet(x => x["SMTP:Host"]).Returns("smtp.googlemail.com");
            _configurationMock.SetupGet(x => x["SMTP:Port"]).Returns("587"); // Replace with your port
            _configurationMock.SetupGet(x => x["SMTP:MailAddress"]).Returns("anwar@dev-bee.com");
            _configurationMock.SetupGet(x => x["SMTP:Password"]).Returns("qxhjizsoboiktfxg");
            _configurationMock.SetupGet(x => x["SMTP:UserName"]).Returns("RIMOT-Evac Test");

            var emailService = new EmailService(_configurationMock.Object);

            var toAddress = "aanwar.1470@gmail.com";
            var subject = "Test Subject";
            var mailBody = "Test Body";

            //  _smtpClientMock.Setup(x => x.Send(It.IsAny<MailMessage>()));

            // Act
            emailService.SendEmail(toAddress, subject, mailBody);

            // Assert
            //_smtpClientMock.Verify(x => x.Send(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}