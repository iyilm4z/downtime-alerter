using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DowntimeAlerter.Reflection
{
    public class AppTypeFinder : ITypeFinder
    {
        private readonly IAssemblyProvider _assemblyProvider;

        public AppTypeFinder(IAssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }

        private static bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();

                return (from implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null)
                        where implementedInterface.IsGenericType
                        select genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                    .FirstOrDefault();
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true) =>
            FindClassesOfType(typeof(T), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true) =>
            FindClassesOfType(assignTypeFrom, _assemblyProvider.GetAssemblies(), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true) =>
            FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();

            try
            {
                foreach (var assembly in assemblies)
                {
                    Type[] types = null;

                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch
                    {
                        // ignored
                    }

                    if (types == null)
                        continue;

                    foreach (var type in types)
                        if (assignTypeFrom.IsAssignableFrom(type) ||
                            assignTypeFrom.IsGenericTypeDefinition &&
                            DoesTypeImplementOpenGeneric(type, assignTypeFrom))
                            if (!type.IsInterface)
                                if (onlyConcreteClasses)
                                {
                                    if (type.IsClass && !type.IsAbstract)
                                        result.Add(type);
                                }
                                else
                                    result.Add(type);
                }
            }
            catch
            {
                // ignored
            }

            return result;
        }

        public IEnumerable<Assembly> Assemblies => _assemblyProvider.GetAssemblies();
    }
}