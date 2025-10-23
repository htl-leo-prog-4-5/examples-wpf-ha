using System;

namespace Enterprise.Shared
{
    public sealed class ScopeInstance<T> : IScope<T>, IDisposable where T : class
    {
        private readonly T _instance;

        private bool _isDisposed;

        public ScopeInstance(T instance)
        {
            _instance = instance;
        }

        public T Instance
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException("this", "Instance is not valid after Dispose.");
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
        }
    }
}