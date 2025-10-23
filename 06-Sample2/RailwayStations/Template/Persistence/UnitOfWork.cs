using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ICityRepository           CityRepository           { get; }
    public IInfrastructureRepository InfrastructureRepository { get; }
    public IRailwayCompanyRepository RailwayCompanyRepository { get; }
    public IStationRepository        StationRepository        { get; }
    public ILineRepository           LineRepository           { get; }

    public UnitOfWork(ApplicationDbContext context,
        ICityRepository                    cityRepository,
        IInfrastructureRepository          infrastructureRepository,
        IRailwayCompanyRepository          railwayCompanyRepository,
        IStationRepository                 stationRepository,
        ILineRepository                    lineRepository
    ) : base(context)
    {
        CityRepository           = cityRepository;
        InfrastructureRepository = infrastructureRepository;
        RailwayCompanyRepository = railwayCompanyRepository;
        StationRepository        = stationRepository;
        LineRepository           = lineRepository;
    }
}