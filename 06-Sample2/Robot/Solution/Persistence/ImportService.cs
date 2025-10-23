namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;
using System;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<int> ImportRaceAsync(string driverName, string competitionName, string carName, DateTime raceStartTime, TimeOnly raceTime, string moves)
    {
        var driver = await _uow.Driver.GetByNameAsync(driverName);

        if (driver == null)
        {
            driver = new Driver()
            {
                Name = driverName
            };
        }

        var competition = await _uow.Competition.GetByNameAsync(competitionName);

        if (competition == null)
        {
            competition = new Competition()
            {
                Name = competitionName
            };
        }

        var car = await _uow.Car.GetByNameAsync(carName);

        if (car == null)
        {
            car = new Car()
            {
                Name = carName
            };
        }

        var race = new Race()
        {
            Competition   = competition,
            Driver        = driver,
            Car           = car,
            RaceStartTime = raceStartTime,
            RaceTime      = raceTime,
            Moves = moves.Split(',').Select((m, index) => new Move()
            {
                Direction = int.Parse(m),
                Duration  = 250,
                Speed     = 200,
                No        = index + 1
            }).ToList()
        };

        await _uow.Race.AddAsync(race);
        await _uow.SaveChangesAsync();

        return race.Moves.Count;
    }
}