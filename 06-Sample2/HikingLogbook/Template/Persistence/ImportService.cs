namespace Persistence;

using Core.Contracts;

using System.Threading.Tasks;

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
        var hikeCsv = await (new CsvImport<HikeCsv>().ReadAsync("ImportData/MyHikes.txt"));
    }
}