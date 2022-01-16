using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Engine;
using DowntimeAlerter.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DowntimeAlerter.Web
{
    public class WepApiStartup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private IEngine _engine;
        private AppConfig _appConfig;

        public WepApiStartup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            (_engine, _appConfig) = services.ConfigureApplicationServices(_configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            if (_env.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.ConfigureRequestPipeline();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            _engine.RegisterDependencies(builder, _appConfig);
        }
    }
}
