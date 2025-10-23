namespace Persistence;

using Core.Contracts;

using System.Threading.Tasks;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportScriptAsync(string name, string? description, string? origin, DateOnly creationDate, string fileName)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}