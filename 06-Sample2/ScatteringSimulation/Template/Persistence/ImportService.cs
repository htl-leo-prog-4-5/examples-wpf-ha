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

    }

    public async Task ImportSampleAsync(string fileName)
    {
    }
}