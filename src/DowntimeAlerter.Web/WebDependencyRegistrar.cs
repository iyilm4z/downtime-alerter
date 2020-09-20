using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Reflection;
using DowntimeAlerter.Web.Factories.Home;

namespace DowntimeAlerter.Web
{
    public class WebDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.RegisterType<HomeModelFactory>()
                .As<IHomeModelFactory>()
                .InstancePerLifetimeScope();
        }

        public int Order => 8;
    }
}
