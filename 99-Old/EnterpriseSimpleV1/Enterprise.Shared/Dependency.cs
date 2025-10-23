using System;

using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Shared
{
    public class Dependency
    {
        public static IServiceCollection Container;


        public static void RegisterType<TFrom, TTo>() where TFrom : class where TTo : class, TFrom
        {
            Container.AddTransient<TFrom, TTo>();
        }


        public static void RegisterInstance<TFrom>(TFrom obj) where TFrom : class
        {
            Container.AddSingleton<TFrom>(obj);
        }

        public static TInterface Resolve<TInterface>()
        {
            return (TInterface)Container.BuildServiceProvider().GetService(typeof(TInterface));
        }
    }
}