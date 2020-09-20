using Autofac;
using DowntimeAlerter.Authorization;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter
{
    public class DomainDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.RegisterType<CookieAuthenticationManager>()
                .As<IAuthenticationManager>()
                .InstancePerLifetimeScope();
        }

        public int Order => 3;
    }
}
