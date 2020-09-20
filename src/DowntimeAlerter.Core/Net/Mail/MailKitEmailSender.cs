using System;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Logging;
using MailKit.Net.Smtp;
using MimeKit;

namespace DowntimeAlerter.Net.Mail
{
    public class MailKitEmailSender : IEmailSender
    {
        private readonly EmailConfig _mailConfig;
        private readonly ILogger _logger;

        public MailKitEmailSender(EmailConfig mailConfig, ILogger logger)
        {
            _mailConfig = mailConfig;
            _logger = logger;
        }

        private MimeMessage CreateEmailMessage(MailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_mailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_mailConfig.SmtpServer, _mailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_mailConfig.UserName, _mailConfig.Password);
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    _logger.Error($"A mail couldn't be sent.", ex);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public void SendEmail(MailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
    }
}
