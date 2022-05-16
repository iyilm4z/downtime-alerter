using DowntimeAlerter.Net.Mail;

namespace DowntimeAlerter.Notification
{
    public class DefaultNotifier : INotifier
    {
        private readonly IEmailSender _emailSender;

        public DefaultNotifier(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        private static MailMessage MapToMailMessage(NotifyModel notifyModel)
        {
            return new MailMessage(notifyModel.To, notifyModel.Subject, notifyModel.Content);
        }

        public void Nofify(NotifyModel notifyModel)
        {
            var emailModel = MapToMailMessage(notifyModel);

            _emailSender.SendEmail(emailModel);
        }
    }
}