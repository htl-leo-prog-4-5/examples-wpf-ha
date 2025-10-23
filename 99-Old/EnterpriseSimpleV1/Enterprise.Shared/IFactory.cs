using System;

namespace Enterprise.Shared
{
    public interface IScope<T> : IDisposable where T : class
    {
        T Instance { get; }
    }

    public interface IFactory<T> where T : class
    {
        IScope<T> Create();
    }
}