using Autofac;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Logging;
using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter
{
    public class ApplicationDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.RegisterType<UserAppService>()
                .As<IUserAppService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TargetApplicationAppService>()
                .As<ITargetApplicationAppService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DefaultLogger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LogAppService>()
                .As<ILogAppService>()
                .InstancePerLifetimeScope();
        }

        public int Order => 6;
    }
}
