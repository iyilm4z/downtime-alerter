﻿using Autofac;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Reflection;

namespace DowntimeAlerter.Web
{
    public class WebAdminDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppConfig config)
        {
        }

        public int Order => 8;
    }
}
