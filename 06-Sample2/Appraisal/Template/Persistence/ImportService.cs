namespace Persistence;

using System.Globalization;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;

using Persistence.ImportData;

using System.Threading.Tasks;

using Core.Tools;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<int> ImportDoctorsAsync(string fileName)
    {
        //TODO Import Doctors
        throw new NotImplementedException();
    }

    public async Task<int> ImportAsync(string directory)
    {
        int newCount    = 0;
        int existsCount = 0;

        foreach (var fileName in Directory.GetFiles(directory, "*.txt"))
        {
            if (await ImportFileAsync(fileName))
            {
                newCount++;
            }
            else
            {
                existsCount++;
            }
        }

        return newCount;
    }

    public async Task<bool> ImportFileAsync(string fileName)
    {
        //TODO Import Examinations
        throw new NotImplementedException();
    }
}