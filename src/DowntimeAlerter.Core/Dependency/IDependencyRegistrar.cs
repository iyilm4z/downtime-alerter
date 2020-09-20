using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter.Dependency
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig appConfig);

        int Order { get; }
    }
}
