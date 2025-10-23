using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Unity;

namespace GradeCalc.Core
{
    internal static class Dependency
    {
        private static readonly object _mutex = new object();
        private static bool _initDone;
        private static IUnityContainer _container;

        public static void InitDependencies()
        {
            lock (_mutex)
            {
                if (_initDone)
                {
                    throw new InvalidOperationException("Dependency initialization run twice");
                }

                InternalInitDependencies();
                _initDone = true;
            }
        }

        public static TInt Resolve<TInt>()
        {
            lock (_mutex)
            {
                return _container.Resolve<TInt>();
            }
        }

        public static void RegisterInstance<TInterface>(TInterface instance)
        {
            _container.RegisterInstance(instance);
        }

        public static void RegisterType<TInterface, TType>() where TType : TInterface
        {
            _container.RegisterType<TInterface, TType>();
        }

        private static void InternalInitDependencies()
        {
            string GetConventionTypeName(Type t)
            {
                var interfaceName = t?.Name;
                if (string.IsNullOrWhiteSpace(interfaceName) || !interfaceName.StartsWith("I") ||
                    interfaceName.Length - 1 == 0)
                {
                    return null;
                }
                return interfaceName.Substring(1);
            }

            List<TypeInfo> allTypes = typeof(Dependency).Assembly.DefinedTypes.ToList();
            Dictionary<string, TypeInfo> interfaceLookup = allTypes.Where(t => t.IsInterface)
                .Select(t => (name:GetConventionTypeName(t), type:t)).Where(tup => !string.IsNullOrEmpty(tup.name))
                .ToDictionary(tup => tup.name, tup => tup.type);
            var container = new UnityContainer();

            foreach (var implType in allTypes.Where(t => !t.IsInterface && !t.IsAbstract))
            {
                var typeName = implType.Name;
                if (!interfaceLookup.TryGetValue(typeName, out var iType) || !iType.IsAssignableFrom(implType))
                {
                    if (typeName.EndsWith("ViewModel"))
                    {
                        container.RegisterType(implType);
                    }
                    continue;
                }
                container.RegisterType(iType, implType);
            }

            ProcessCustomRegistrations(container);

            _container = container;
        }

        private static void ProcessCustomRegistrations(IUnityContainer container)
        {
            container.RegisterInstance(new HttpClient());
        }
    }
}