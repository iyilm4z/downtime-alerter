using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Startup
{
    public class CommonStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddAppAntiForgery();
            services.AddRouting();
            services.AddHostedService<TargetApplicationBackgroundJob>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseStaticFiles();
            application.UseRouting();
        }

        public int Order => 100;
    }
}
