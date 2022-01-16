using System;
using System.Linq;
using System.Net;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Engine;
using DowntimeAlerter.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static (IEngine, AppConfig) ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var appConfig = services.ConfigureStartupConfig<AppConfig>(configuration.GetSection("App"));
            services.ConfigureStartupConfig<EmailConfig>(configuration.GetSection("Email"));

            services.AddHttpContextAccessor();
            services.AddMvcCore();

            var engine = EngineContext.Create();
            engine.ConfigureServices(services, configuration, appConfig);

            return (engine, appConfig);
        }

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();

            configuration.Bind(config);

            services.AddSingleton(config);

            return config;
        }

        public static void AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(AppDefaults.Authentication.AuthenticationScheme)
                .AddCookie(AppDefaults.Authentication.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = $"{AppDefaults.Cookie.Prefix}{AppDefaults.Cookie.Authentication}";
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = AppDefaults.Authentication.LoginPath;
                });
        }

        public static IMvcBuilder AddAppMvc(this IServiceCollection services)
        {
            var mvcBuilder = services.AddControllersWithViews();

            mvcBuilder.AddRazorRuntimeCompilation();

            services.AddRazorPages();

            mvcBuilder.AddFluentValidation(configuration =>
            {
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("DowntimeAlerter", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);

                configuration.RegisterValidatorsFromAssemblies(assemblies);

                configuration.ImplicitlyValidateChildProperties = true;
            });

            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }

        public static void AddAppDbContext(this IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServerWithLazyLoading(services);
            });

            //services.AddDbContext<AppDbContext>(optionsBuilder =>
            //{
            //    optionsBuilder.UseSqlServerWithLazyLoading(services);
            //}, ServiceLifetime.Transient);
        }

        public static void AddAppAntiForgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = $"{AppDefaults.Cookie.Prefix}{AppDefaults.Cookie.Antiforgery}";
            });
        }
    }
}