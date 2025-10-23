namespace Persistence;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;

using Base.Tools.CsvImport;

using Persistence.ImportData;

using System.Globalization;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportBaseDataAsync()
    {
        var categoriesCsv = await (new CsvImport<CategoryCsv>().ReadAsync("ImportData/Categories.txt"));
        var originsCsv    = await (new CsvImport<OriginCsv>().ReadAsync("ImportData/Origins.txt"));

        await _uow.CategoryRepository.AddRangeAsync(categoriesCsv.Select(c => new Category()
        {
            Description = c.Name
        }));

        await _uow.OriginRepository.AddRangeAsync(originsCsv.Select(o => new Origin()
        {
            Code = o.Code,
            Name = o.Name
        }));

        await _uow.SaveChangesAsync();
    }

    public async Task ImportSampleAsync(string fileName)
    {
        var sampleInfo = (await (new CsvImport<SampleInfoCsv>().ReadAsync(fileName))).ToDictionary(e => e.Key);

        var sampleDataCsv = await (new CsvImport<SampleDataCsv>().ReadAsync($"{Path.GetDirectoryName(fileName)}\\{sampleInfo["DataFile"].Value}"));

        var     originsInDb = await _uow.OriginRepository.GetAsync();
        Origin? origin      = null;

        if (sampleInfo.ContainsKey("Origin"))
        {
            origin = originsInDb.SingleOrDefault(o => sampleInfo["Origin"].Value == o.Code);
            origin ??= new Origin()
            {
                Code = sampleInfo["Origin"].Value,
                Name = sampleInfo["Origin"].Value
            };
        }

        await _uow.SimulationRepository.AddAsync(
            new Simulation()
            {
                CreationDate = DateOnly.FromDateTime(DateTime.ParseExact(sampleInfo["CreationDate"].Value, "yyyy.MM.dd", CultureInfo.InvariantCulture)),
                Description  = sampleInfo["Description"].Value,
                Name         = Path.GetFileName(fileName),
                Origin       = origin,
                Samples = sampleDataCsv.Select((csv, idx) => new Sample()
                {
                    SeqNo = idx + 1,
                    X     = csv.X,
                    Y     = csv.Y,
                    Value = csv.Value
                }).ToList()
            });

        await _uow.SaveChangesAsync();
    }
}