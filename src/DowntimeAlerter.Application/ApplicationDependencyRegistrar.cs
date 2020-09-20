using Autofac;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
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
        }

        public int Order => 6;
    }
}
