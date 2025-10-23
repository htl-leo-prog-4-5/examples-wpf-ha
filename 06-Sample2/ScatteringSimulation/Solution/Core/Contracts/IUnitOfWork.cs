namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ISampleRepository     SampleRepository     { get; }
    public ICategoryRepository   CategoryRepository   { get; }
    public ISimulationRepository SimulationRepository { get; }
    public IOriginRepository     OriginRepository     { get; }
}