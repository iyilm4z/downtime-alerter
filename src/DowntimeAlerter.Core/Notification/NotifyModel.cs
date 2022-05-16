using System.Collections.Generic;

namespace DowntimeAlerter.Notification
{
    public class NotifyModel
    {
        public List<string> To { get; }
        public string Subject { get; }
        public string Content { get; }

        public NotifyModel(List<string> to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
    }
}