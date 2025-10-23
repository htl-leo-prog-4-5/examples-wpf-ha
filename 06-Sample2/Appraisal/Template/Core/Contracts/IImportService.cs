namespace Core.Contracts;

using System.Threading.Tasks;

public interface IImportService
{
    Task<int>  ImportAsync(string     directory);
    Task<bool> ImportFileAsync(string fileName);

    Task<int> ImportDoctorsAsync(string fileName);
}