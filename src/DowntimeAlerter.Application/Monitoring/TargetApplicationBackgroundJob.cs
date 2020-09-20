using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Logging;
using DowntimeAlerter.Net.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DowntimeAlerter.Monitoring
{
    // This is singleton instanced background job
    public class TargetApplicationBackgroundJob : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IEmailSender _emailSender;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        private Timer _timer;
        private bool _isJobRunning;

        public TargetApplicationBackgroundJob(IServiceScopeFactory serviceScopeFactory,
            IEmailSender emailSender,
            ILogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _emailSender = emailSender;
            _logger = logger;
            _httpClient = new HttpClient();
        }

        private void CheckHealth(object state)
        {
            if (_isJobRunning)
                return;

            _isJobRunning = true;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var targetApplicationRepository = scope.ServiceProvider.GetRequiredService<IRepository<TargetApplication>>();
                var targetApplications = targetApplicationRepository.GetAllList();

                var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
                var users = userRepository.GetAllList();
                var userEmails = users.Select(x => x.Email).ToList();

                foreach (var app in targetApplications)
                {
                    var lastCheckDate = app.LastCheckDate ?? DateTime.Now.AddSeconds(-(int)app.Interval);

                    var totalSecondsDiff = (DateTime.Now - lastCheckDate).TotalSeconds;
                    if (!(totalSecondsDiff >= (int)app.Interval))
                        continue;

                    try
                    {
                        var responseMessage = _httpClient.GetAsync(app.Url).GetAwaiter().GetResult();
                        if (responseMessage.StatusCode != HttpStatusCode.OK)
                        {
                            var message = new MailMessage(userEmails, $"{AppDefaults.AppName} Notification", $"{app.Name} is DOWN.");
                            _emailSender.SendEmail(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"An error occured for {app.Name} while checking health of it.", ex);

                        continue;
                    }

                    app.LastCheckDate = DateTime.Now;
                    targetApplicationRepository.Update(app);
                }
            }

            _isJobRunning = false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Run every 5 seconds. Interval should be lower than MonitorInterval.Low value. 
            _timer = new Timer(CheckHealth, null, 0, 5000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // New Timer does not have a stop. 
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}