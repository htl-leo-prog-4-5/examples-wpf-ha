namespace Core.Contracts;

using System.Threading.Tasks;

public interface IImportService
{
    Task ImportBaseDataAsync();

    Task ImportSampleAsync(string fileName);
}