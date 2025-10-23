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

        await _uow.SaveChangesAsync();
    }
}