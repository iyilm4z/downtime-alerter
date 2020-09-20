using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Startup
{
    public class DbStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppDbContext();
            services.AddEntityFrameworkSqlServer();
            services.AddEntityFrameworkProxies();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 10;
    }
}
