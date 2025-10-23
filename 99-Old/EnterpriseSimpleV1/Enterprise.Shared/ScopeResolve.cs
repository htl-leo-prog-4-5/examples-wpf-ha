using System;

using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Shared
{
    // Factory/Scope using Resolve of dependencyInjection

    public sealed class ScopeResolve<T> : IScope<T>, IDisposable where T : class
    {
        private readonly IServiceScope _scope;
        private readonly T             _instance;

        private bool _isDisposed;

        public ScopeResolve(IServiceScope scope, T instance)
        {
            _scope    = scope;
            _instance = instance;
        }

        public T Instance
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("this", "Dispose must not be called twice.");
                }

                return _instance;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            _scope.Dispose();
        }
    }
}