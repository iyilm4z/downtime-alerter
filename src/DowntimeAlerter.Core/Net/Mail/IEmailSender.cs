namespace DowntimeAlerter.Net.Mail
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
    }
}