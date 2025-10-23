using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Shared
{
    // Factory/Scope using Resolve of dependencyInjection

    public class FactoryResolve<T> : IFactory<T> where T : class
    {
        private readonly IServiceCollection _container;

        public FactoryResolve()
        {
            _container = Dependency.Container;
        }

        public IScope<T> Create()
        {
            var childContainer = _container.BuildServiceProvider().CreateScope();
            return new ScopeResolve<T>(childContainer, childContainer.ServiceProvider.GetService<T>());
        }
    }
}