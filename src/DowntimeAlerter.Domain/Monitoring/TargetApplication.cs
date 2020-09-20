using System;
using DowntimeAlerter.Domain.Entities;

namespace DowntimeAlerter.Monitoring
{
    public class TargetApplication : Entity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public MonitorInterval Interval { get; set; }

        public DateTime? LastCheckDate { get; set; }
    }
}
