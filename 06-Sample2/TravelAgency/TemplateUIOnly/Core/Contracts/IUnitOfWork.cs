namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IHotelRepository     HotelRepository     { get; }
    public IPlaneRepository     PlaneRepository     { get; }
    public IRouteStepRepository RouteStepRepository { get; }
    public IRouteRepository     RouteRepository     { get; }
    public ITripRepository      TripRepository      { get; }
    public IShipRepository      ShipRepository      { get; }
}