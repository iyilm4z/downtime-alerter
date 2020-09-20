using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Startup
{
    public class AuthenticationStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppAuthentication();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseAppAuthentication();
        }

        public int Order => 500;
    }
}
