using System;
using Unity;
using Unity.Extension;

namespace Enterprise.Shared
{
    public class Dependency
    {
        public static IUnityContainer Container = new UnityContainer();


        public static void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            Container.RegisterType<TFrom, TTo>();
        }


        public static void RegisterInstance<TFrom>(TFrom obj)
        {
            Container.RegisterInstance<TFrom>(obj);
        }
    }
}
