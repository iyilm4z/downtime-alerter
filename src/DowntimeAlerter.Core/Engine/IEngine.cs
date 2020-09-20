using System;
using System.Collections.Generic;
using Autofac;
using DowntimeAlerter.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Engine
{
    public interface IEngine
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, AppConfig appConfig);

        void ConfigureRequestPipeline(IApplicationBuilder application);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();

        object ResolveUnregistered(Type type);

        void RegisterDependencies(ContainerBuilder containerBuilder, AppConfig appConfig);
    }
}