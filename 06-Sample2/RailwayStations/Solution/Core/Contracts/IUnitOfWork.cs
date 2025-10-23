namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICityRepository           CityRepository           { get; }
    public IRailwayCompanyRepository RailwayCompanyRepository { get; }
    public IInfrastructureRepository InfrastructureRepository { get; }
    public IStationRepository        StationRepository        { get; }
    public ILineRepository           LineRepository           { get; }
}