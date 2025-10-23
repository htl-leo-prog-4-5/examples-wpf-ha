namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;
using System;

using Base.Tools;
using Base.Tools.CsvImport;

using Core;

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
        var gamesCsv = await new CsvImport<GamesCsv>().ReadAsync("ImportData/Games.csv");
        var game = gamesCsv
            .Select(g =>
            {
                var normalized = new byte[] { g.No1, g.No2, g.No3, g.No4, g.No5, g.No6, }.Normalize().ToArray();
                return new Game()
                {
                    ExpectedDrawDate = g.Date,
                    DrawDate         = g.Date.ToDateTime(TimeOnly.MinValue),
                    DateFrom         = g.Date.AddDays(-7),
                    DateTo           = g.Date.AddDays(-1),
                    No1              = normalized[0],
                    No2              = normalized[1],
                    No3              = normalized[2],
                    No4              = normalized[3],
                    No5              = normalized[4],
                    No6              = normalized[5],
                    NoX              = g.ZZ
                };
            });

        var officeCsv = await new CsvImport<OfficeCsv>().ReadAsync("ImportData/Office.csv");
        var office = officeCsv
            .Select(g => new Office()
            {
                No      = g.No.ToString(),
                Name    = g.Name,
                Address = g.Bundesland
            });

        await _uow.GameRepository.AddRangeAsync(game);
        await _uow.OfficeRepository.AddRangeAsync(office);

        await _uow.SaveChangesAsync();
    }
}