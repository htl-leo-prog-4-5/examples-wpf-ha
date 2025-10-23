namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;

using Base.Tools.CsvImport;

using Persistence.ImportData;

public class ImportService : IImportService
{
    public ImportService()
    {
    }

    public async Task ImportDbAsync()
    {
        //We need an uow => use constructor DI 

        var stationCsv          = await (new CsvImport<BahnhofCsv>().ReadAsync("ImportData/Bahnhöfe.txt"));
        var infrastructuresCsv  = await (new CsvImport<IBCsv>().ReadAsync("ImportData/IB.txt"));
        var railwayCompaniesCsv = await (new CsvImport<EVUCsv>().ReadAsync("ImportData/EVU.txt"));

        string[] SplitAndTrim(string str)
        {
            return str.Split(',').Select(s => s.Trim()).ToArray();
        }


        //TODO: To get all "Railway-Line" entities, we can use the following code:
        /*
                var lines = stationCsv
                    .SelectMany(s => SplitAndTrim(s.Strecke))
                    .Distinct()
                    .Select(s => new Line()
                    {
                        Name = s
                    })
                    .ToList();
        */

        //TODO: To get all "RailwayCompany" entities, use railwayCompaniesCsv (use Select)

        //TODO: To Convert the column Art (Type) use the following example

        /*
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
                    // same behavior as ConvertArt
                }
        */

        //TODO: now we can create the station entities from each line of the CSV file (use Select)
        // Assign properties:
        //  Code       = ConvertAbk(s.Abk)
        //  IsRegional = string.Compare(s.RV, "RV") == 0
        //  Infrastructures  = infrastructures.Where(x => SplitAndTrim(s.IB).Contains(x.Code)).ToList()
        //  City             = cities[s.Standortgemeinde.Trim()]
        //  ... and so on

        //await _uow.StationRepository.AddRangeAsync(stations);
        //await _uow.SaveChangesAsync();
    }
}