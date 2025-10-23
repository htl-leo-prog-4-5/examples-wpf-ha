namespace Persistence;

using Base.Tools.CsvImport;

using Core.Contracts;

using System.Threading.Tasks;

using Core.Entities;

using Persistence.ImportData;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportDbAsync()
    {
        var shipsCsv  = await new CsvImport<ShipCsv>().ReadAsync("ImportData/Ship.csv");
        var planesCsv = await new CsvImport<PlaneCsv>().ReadAsync("ImportData/Plane.csv");
        var hotelsCsv = await new CsvImport<HotelCsv>().ReadAsync("ImportData/Hotel.csv");
        var routeCsv  = await new CsvImport<RouteCsv>().ReadAsync("ImportData/Route.csv");
        var tripCsv  = await new CsvImport<TripCsv>().ReadAsync("ImportData/Trip.csv");

        var ships = shipsCsv
            .Select(s => new Ship()
            {
                Name              = s.Name,
                Owner             = s.Owner,
                PassengerCapacity = s.PassengerCapacity,
                CargoCapacity     = s.CargoCapacity,
                MaxSpeed          = s.MaxSpeed
            })
            .ToDictionary(s => s.Name);

        var planes = planesCsv
            .Select(p => new Plane()
            {
                Model             = p.Model,
                Manufacturer      = p.Manufacturer,
                PassengerCapacity = p.PassengerCapacity,
                CargoCapacity     = p.CargoCapacity,
                MaxSpeed          = p.MaxSpeed
            })
            .ToDictionary(s => s.Model);

        var hotels = hotelsCsv
            .Select(h => new Hotel()
            {
                Name          = h.Name,
                Rating        = h.Rating,
                PricePerNight = h.PricePerNight,
                Location      = h.Location
            })
            .ToDictionary(s => s.Name);

        var routes = routeCsv
            .Select(r => r.Name)
            .Distinct()
            .Select(r => new Route()
            {
                Name = r
            })
            .ToDictionary(r => r.Name);

        var routeSteps = routeCsv
            .Select(r =>
            {
                Hotel? hotel = r.TransportType == "Hotel" ? hotels[r.TransportInfo] : null;
                Ship?  ship  = r.TransportType == "Ship" ? ships[r.TransportInfo] : null;
                Plane? plane = r.TransportType == "Plane" ? planes[r.TransportInfo] : null;

                return new RouteStep()
                {
                    Description = r.Description,
                    Route       = routes[r.Name],
                    Hotel       = hotel,
                    Ship        = ship,
                    Plane       = plane
                };
            })
            .ToList();

        if (routeSteps.Count > 0)
        {
            var lastRoute = routeSteps.First().Route;
            int no        = 1;

            foreach (var routeStep in routeSteps)
            {
                if (!ReferenceEquals(lastRoute,routeStep.Route))
                {
                    no        = 1;
                    lastRoute = routeStep.Route;
                }

                routeStep.No = no;
                no++;
            }
        }

        var trips = tripCsv
            .Select(t => new Trip()
            {
                Route             = routes[t.RouteName],
                DepartureDateTime = t.DepartureDateTime,
                ArrivalDateTime   = t.ArrivalDateTime
            })
            .ToList();

        await _uow.ShipRepository.AddRangeAsync(ships.Values);
        await _uow.PlaneRepository.AddRangeAsync(planes.Values);
        await _uow.HotelRepository.AddRangeAsync(hotels.Values);
        await _uow.RouteStepRepository.AddRangeAsync(routeSteps);
        await _uow.TripRepository.AddRangeAsync(trips);

        await _uow.SaveChangesAsync();
    }
}