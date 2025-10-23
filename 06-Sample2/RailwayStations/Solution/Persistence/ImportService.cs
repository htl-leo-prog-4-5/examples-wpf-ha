namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;

using Base.Tools.CsvImport;

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
        var stationCsv          = await (new CsvImport<BahnhofCsv>().ReadAsync("ImportData/Bahnhöfe.txt"));
        var infrastructuresCsv  = await (new CsvImport<IBCsv>().ReadAsync("ImportData/IB.txt"));
        var railwayCompaniesCsv = await (new CsvImport<EVUCsv>().ReadAsync("ImportData/EVU.txt"));

        string[] SplitAndTrim(string str)
        {
            return str.Split(',').Select(s => s.Trim()).ToArray();
        }

        var infrastructures = infrastructuresCsv
            .Select(i => new Infrastructure()
            {
                Code = i.Code,
                Name = i.Name
            })
            .ToList();

        var railwayCompanies = railwayCompaniesCsv
            .Select(i => new RailwayCompany()
            {
                Code = i.Code,
                Name = i.Name
            })
            .ToList();

        var cities = stationCsv
            .Select(s => s.Standortgemeinde.Trim())
            .Distinct()
            .Select(s => new City()
            {
                Name = s
            })
            .ToDictionary(s => s.Name);

        var lines = stationCsv
            .SelectMany(s => SplitAndTrim(s.Strecke))
            .Distinct()
            .Select(s => new Line()
            {
                Name = s
            })
            .ToList();

        string? ConvertArt(string? art)
        {
            if (string.IsNullOrEmpty(art) || art == "-")
            {
                return null;
            }

            return art;
        }

        string? ConvertAbk(string? abk)
        {
            if (string.IsNullOrEmpty(abk) || abk == "-")
            {
                return null;
            }

            return abk;
        }

        var stations = stationCsv.Select(s => new Station()
        {
            Code             = ConvertAbk(s.Abk),
            Name             = s.Name,
            StateCode        = s.BL,
            IsRegional       = string.Compare(s.RV, "RV") == 0,
            IsIntercity      = string.Compare(s.FV, "FV") == 0,
            IsExpress        = string.Compare(s.SB, "SB") == 0,
            Remark           = string.IsNullOrEmpty(s.Bemerkungen) ? null : s.Bemerkungen,
            Type             = ConvertArt(s.Art),
            Lines            = lines.Where(x => SplitAndTrim(s.Strecke).Contains(x.Name)).ToList(),
            Infrastructures  = infrastructures.Where(x => SplitAndTrim(s.IB).Contains(x.Code)).ToList(),
            RailwayCompanies = railwayCompanies.Where(x => SplitAndTrim(s.EVU).Contains(x.Code)).ToList(),
            City             = cities[s.Standortgemeinde.Trim()]
        }).ToList();

        await _uow.StationRepository.AddRangeAsync(stations);
        await _uow.SaveChangesAsync();
    }
}