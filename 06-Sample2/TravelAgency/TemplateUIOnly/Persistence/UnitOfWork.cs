using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ITripRepository      TripRepository      { get; }
    public IRouteRepository     RouteRepository     { get; }
    public IRouteStepRepository RouteStepRepository { get; }
    public IHotelRepository     HotelRepository     { get; }
    public IPlaneRepository     PlaneRepository     { get; }
    public IShipRepository      ShipRepository      { get; }

    public UnitOfWork(ApplicationDbContext context,
        ITripRepository                    tripRepository,
        IRouteRepository                   routeRepository,
        IRouteStepRepository               routeStepRepository,
        IHotelRepository                   hotelRepository,
        IShipRepository                    shipRepository,
        IPlaneRepository                   planeRepository
    ) : base(context)
    {
        TripRepository      = tripRepository;
        PlaneRepository     = planeRepository;
        ShipRepository      = shipRepository;
        HotelRepository     = hotelRepository;
        RouteRepository     = routeRepository;
        RouteStepRepository = routeStepRepository;
    }
}