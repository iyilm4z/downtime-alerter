using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using DowntimeAlerter.Configuration;
using DowntimeAlerter.Dependency;
using DowntimeAlerter.Mapper;
using DowntimeAlerter.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DowntimeAlerter.Engine
{
    public class AppEngine : IEngine
    {
        private IServiceProvider _serviceProvider;
        private IAssemblyProvider _assemblyProvider;
        private ITypeFinder _typeFinder;

        private IServiceProvider GetServiceProvider()
        {
            var accessor = _serviceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;

            return context?.RequestServices ?? _serviceProvider;
        }

        private static void AddAutoMapper(ITypeFinder typeFinder)
        {
            var mapperConfigurations = typeFinder.FindClassesOfType<IOrderedMapperProfile>();

            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IOrderedMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            AutoMapperConfiguration.Init(config);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            var typeFinder = Resolve<ITypeFinder>();
            if (typeFinder == null)
                return null;

            assembly = typeFinder.Assemblies.FirstOrDefault(asm => asm.FullName == args.Name);

            return assembly;
        }

        private static void CallConfigureServicesMethodOfStartups(IServiceCollection services, IConfiguration configuration, ITypeFinder typeFinder)
        {
            var startups = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startups
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);
        }

        private static void CallConfigureMethodOfStartups(IApplicationBuilder application, ITypeFinder typeFinder)
        {
            var startups = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startups
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.Configure(application);
        }

        private void RunStartupTasks(ITypeFinder typeFinder)
        {
            var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            var instances = startupTasks
                .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask.Order);

            foreach (var task in instances)
                task.Execute();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, AppConfig appConfig)
        {
            _assemblyProvider = new AppAssemblyProvider();
            _typeFinder = new AppTypeFinder(_assemblyProvider);

            CallConfigureServicesMethodOfStartups(services, configuration, _typeFinder);

            AddAutoMapper(_typeFinder);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            _serviceProvider = application.ApplicationServices;

            var typeFinder = Resolve<ITypeFinder>();

            CallConfigureMethodOfStartups(application, typeFinder);

            RunStartupTasks(_typeFinder);
        }

        public T Resolve<T>() where T : class => (T)Resolve(typeof(T));

        public object Resolve(Type type) => GetServiceProvider().GetService(type);

        public IEnumerable<T> ResolveAll<T>() => (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;

            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new Exception("Unknown dependency");

                        return service;
                    });

                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
        }

        public void RegisterDependencies(ContainerBuilder containerBuilder, AppConfig appConfig)
        {
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();
            containerBuilder.RegisterInstance(_assemblyProvider).As<IAssemblyProvider>().SingleInstance();
            containerBuilder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();

            var dependencyRegistrars = _typeFinder.FindClassesOfType<IDependencyRegistrar>();

            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, _typeFinder, appConfig);
        }
    }
}