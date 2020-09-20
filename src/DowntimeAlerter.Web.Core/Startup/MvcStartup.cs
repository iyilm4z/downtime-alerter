using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Startup
{
    public class MvcStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppMvc();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseAppEndpoints();
        }

        public int Order => 1000;
    }
}
