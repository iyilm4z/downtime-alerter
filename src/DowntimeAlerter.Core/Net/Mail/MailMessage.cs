using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace DowntimeAlerter.Net.Mail
{
    public class MailMessage
    {
        public List<MailboxAddress> To { get; }
        public string Subject { get; }
        public string Content { get; }
        public MailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}