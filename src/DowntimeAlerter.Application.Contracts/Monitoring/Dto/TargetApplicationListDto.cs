﻿using DowntimeAlerter.Application.Services.Dto;

namespace DowntimeAlerter.Monitoring.Dto
{
    public class TargetApplicationListDto : EntityDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public MonitorInterval Interval { get; set; }
    }
}