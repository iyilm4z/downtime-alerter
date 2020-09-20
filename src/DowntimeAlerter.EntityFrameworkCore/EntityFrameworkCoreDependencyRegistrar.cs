using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Reflection;
using DowntimeAlerter.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter
{
    public class EntityFrameworkCoreDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.Register(context => new AppDbContext(context.Resolve<DbContextOptions<AppDbContext>>()))
                .As<IDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfCoreRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }

        public int Order => 4;
    }
}
