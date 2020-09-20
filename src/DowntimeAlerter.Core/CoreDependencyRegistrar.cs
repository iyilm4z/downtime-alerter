using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Net.Mail;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter
{
    public class CoreDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
            builder.RegisterType<MailKitEmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}
