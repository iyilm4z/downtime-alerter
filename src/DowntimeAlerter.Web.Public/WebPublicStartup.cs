using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Engine;
using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web
{
    public class WebPublicStartup
    {
        private readonly IConfiguration _configuration;
        private IEngine _engine;
        private AppConfig _appConfig;

        public WebPublicStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            (_engine, _appConfig) = services.ConfigureApplicationServices(_configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.ConfigureRequestPipeline();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            _engine.RegisterDependencies(builder, _appConfig);
        }
    }
}
