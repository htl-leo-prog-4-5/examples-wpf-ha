namespace Enterprise.Shared
{
    public class FactoryInstance<T> : IFactory<T> where T : class
    {
        public FactoryInstance(T obj)
        {
            _obj = obj;
        }

        private readonly T _obj;

        IScope<T> IFactory<T>.Create()
        {
            return new ScopeInstance<T>(_obj);
        }
    }
}