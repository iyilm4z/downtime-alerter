using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter.Web
{
    public class WebCoreDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.RegisterType<AppSession>()
                .As<IAppSession>()
                .InstancePerLifetimeScope();
        }

        public int Order => 7;
    }
}
