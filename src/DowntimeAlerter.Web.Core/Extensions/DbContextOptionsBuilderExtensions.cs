using DowntimeAlerter.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Web.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static void UseSqlServerWithLazyLoading(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services)
        {
            var appConfig = services.BuildServiceProvider().GetRequiredService<AppConfig>();

            var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
            dbContextOptionsBuilder.UseSqlServer(appConfig.DataConnectionString);
        }
    }
}
